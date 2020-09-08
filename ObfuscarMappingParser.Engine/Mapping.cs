using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using ObfuscarMappingParser.Engine.Items;
using ObfuscarMappingParser.Engine.Reader;

namespace ObfuscarMappingParser.Engine
{
  public class Mapping: IEntitySearcher
  {
    private readonly string filename;
    private List<RenamedClass> classes = new List<RenamedClass>();

    private Dictionary<string, RenamedClass> classesCache = new Dictionary<string, RenamedClass>();

    private long loadTime;

    private int methodsCount;
    private int classesCount;
    private int subclassesCount;
    private int skippedCount;

    private HashSet<string> modules = new HashSet<string>();
    private HashSet<string> namespaces = new HashSet<string>();
    private HashSet<string> namespacesObfuscated = new HashSet<string>();

    private static string[] emptyArray = new string[0];

    private bool haveSystemEntities;

    public Mapping(string filename)
    {
      if (!File.Exists(filename))
        throw new ObfuscarParserException("File not exists", filename);

      this.filename = filename;
      LoadFile(new XmlMappingReader(filename)); // FIXME format switch
    }

    private void LoadFile(IMappingReader reader)
    {
      loadTime = 0;
      LoadTimer timer = new LoadTimer("File Parsing");
      reader.Load();

      loadTime += timer.Stop();
      timer = new LoadTimer("Items Processing");

      modules.Clear();
      namespaces.Clear();
      namespacesObfuscated.Clear();
      classes.Clear();
      classesCache.Clear();
      haveSystemEntities = false;
      methodsCount = classesCount = subclassesCount = skippedCount = 0;

      List<RenamedClass> subclasses = new List<RenamedClass>();

      foreach (IMappingEntity entity in reader.Entities)
      {
        RenamedClass c = new RenamedClass(entity, this);
        classesCount++;

        if (c.SkipReason != null)
          skippedCount++;

        if (c.OwnerClassName == null)
        {
          classes.Add(c);
          if (c.Name.NameOld != null && c.Name.NameOld.Namespace != null)
            haveSystemEntities |= c.Name.NameOld.Namespace.StartsWith("System.");
        }
        else
          subclasses.Add(c);

        methodsCount += c.MethodsCount;

        if (c.ModuleNew != null)
          modules.Add(c.ModuleNew);
        if (c.Name.NameOld != null)
        {
          classesCache[c.NameOld] = c;
          classesCache[c.NameOldFull] = c;

          if (!string.IsNullOrEmpty(c.Name.NameOld.Namespace))
            namespaces.Add(c.Name.NameOld.Namespace);
        }
        if (c.Name.NameNew != null)
        {
          classesCache[c.NameNew] = c;
          classesCache[c.NameNewFull] = c;

          if (!string.IsNullOrEmpty(c.Name.NameNew.Namespace))
            namespacesObfuscated.Add(c.Name.NameNew.Namespace);
        }
      }

      loadTime += timer.Stop();
      timer = new LoadTimer("Subclasses Processing");

      foreach (RenamedClass subclass in subclasses)
      {
        RenamedClass c;
        if (classesCache.TryGetValue(subclass.OwnerClassName, out c))
        {
          c.Items.Add(subclass);
          subclass.OwnerClass = c;
          subclassesCount++;
          continue;
        }

        Debug.WriteLine("Failed to find root class: " + subclass.OwnerClassName);
        classes.Add(subclass);
      }

      loadTime += timer.Stop();
      timer = new LoadTimer("Values Updating");

      foreach (RenamedClass c in classes)
        c.UpdateNewNames(this);

      loadTime += timer.Stop();
      Debug.WriteLine("Total Elapsed: {0} ms", loadTime);
    }

    public void Reload()
    {
      LoadFile(new XmlMappingReader(filename)); // FIXME format switch
    }

    public string Filename
    {
      get { return filename; }
    }

    public List<RenamedClass> Classes
    {
      get { return classes; }
    }

    public IEnumerable<string> Modules
    {
      get { return modules; }
    }

    public IEnumerable<string> Namespaces
    {
      get { return namespaces; }
    }

    public IEnumerable<string> NamespacesObfuscated
    {
      get { return namespacesObfuscated; }
    }

    #region Search

    public SearchResults Search(string s, bool allowSubstitution, bool filterPrefix)
    {
      if (string.IsNullOrEmpty(s))
        return null;

      s = s.TrimStart(' ');

      if (filterPrefix)
      {
        int i = s.IndexOf(' ');
        if (i != -1)
          s = s.Substring(i).TrimStart(' ');
      }

      Entity entity = new Entity(s);
      if (entity.EntityType != EntityType.Method && entity.EntityType != EntityType.Constructor)
        return new SearchResults(s, entity, SearchItem(entity.Name));

      return new SearchResults(s, entity, SearchMethod(entity, allowSubstitution));
    }

    public SearchResults SearchOriginal(string s)
    {
      if (string.IsNullOrEmpty(s))
        return null;

      Entity entity = new Entity(s);
      if (entity.EntityType != EntityType.Method && entity.EntityType != EntityType.Constructor)
        return new SearchResults(s, entity, SearchItemOriginal(entity.Name));

      return new SearchResults(s, entity, SearchMethodOriginal(entity));
    }

    private IEnumerable<INamedEntity> SearchMethod(Entity entity, bool allowSubstitution)
    {
      bool hasAdded = false;
      foreach (RenamedBase item in SearchItem(entity.Name))
      {
        if (item.EntityType != EntityType.Method)
          continue;

        RenamedItem i = (RenamedItem)item;
        if (i.CompareParams(entity.MethodParams))
        {
          hasAdded = true;
          yield return i;
        }
      }

      if(hasAdded || !allowSubstitution)
        yield break;

      string[] values = entity.Name.Namespace != null ? entity.Name.Namespace.Split('.') : emptyArray;

      List<EntityName> methodParams = new List<EntityName>(entity.MethodParams);
      SubstituteParams(methodParams);

      foreach (RenamedClass renamedClass in classes)
      {
        foreach (RenamedBase item in renamedClass.Search(values, 0))
          yield return new Entity(entity, item, methodParams);
      }
    }

    private IEnumerable<INamedEntity> SearchMethodOriginal(Entity entity)
    {
      foreach (RenamedBase item in SearchItemOriginal(entity.Name))
      {
        if (item.EntityType != EntityType.Method)
          continue;

        RenamedItem i = (RenamedItem)item;
        if (i.CompareParams(entity.MethodParams))
        {
          yield return i;
        }
      }
    }

    private void SubstituteParams(IList<EntityName> p)
    {
      for (int i = 0; i < p.Count; i++)
      {
        EntityName paramName = null;

        try
        {
          string[] values = p[i].PathName.Split('.');
          foreach (RenamedBase item in SearchItem(values))
            paramName = item.Name.NameOld;
        }
        catch { }

        if (paramName == null)
          foreach (RenamedClass item in SearchClassNoNs(p[i].Name))
            paramName = item.Name.NameOld;

        if (paramName != null)
          p[i] = paramName;
      }
    }

    private IEnumerable<RenamedBase> SearchItem(string[] values)
    {
      foreach (RenamedClass renamedClass in classes)
      {
        foreach (RenamedBase item in renamedClass.Search(values, 0))
          yield return item;
      }
    }

    private IEnumerable<RenamedBase> SearchItem(EntityName name)
    {
      string[] values;
      try
      {
        values = name.PathName.Split('.');
      }
      catch
      {
        yield break;
      }

      foreach (RenamedBase renamedItem in SearchItem(values))
        yield return renamedItem;
    }

    private IEnumerable<RenamedBase> SearchItemOriginal(string[] values)
    {
      foreach (RenamedClass renamedClass in classes)
      {
        foreach (RenamedBase item in renamedClass.SearchOriginal(values, 0))
          yield return item;
      }
    }

    private IEnumerable<RenamedBase> SearchItemOriginal(EntityName name)
    {
      string[] values;
      try
      {
        values = name.PathName.Split('.');
      }
      catch
      {
        yield break;
      }

      foreach (RenamedBase renamedItem in SearchItemOriginal(values))
        yield return renamedItem;
    }

    private IEnumerable<RenamedClass> SearchClassNoNs(string name)
    {
      foreach (RenamedClass c in classes)
      {
        if (string.Compare(c.Name.NameNew.Name, name, StringComparison.Ordinal) == 0)
          yield return c;
      }
    }

    #endregion

    #region Crashlog Processing

    public string ProcessCrashlogText(string text, bool filterPrefix = true)
    {
      List<SearchResults> results = ProcessCrashlog(text, filterPrefix);
      StringBuilder sb = new StringBuilder();
      foreach (SearchResults result in results)
        sb.AppendLine(result.ToString());

      return sb.ToString();
    }

    public List<SearchResults> ProcessCrashlog(string text, bool filterPrefix = true)
    {
      string[] strings = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
      List<SearchResults> results = new List<SearchResults>();

      foreach (string s in strings)
        results.Add(ProcessCrashlogLine(s, filterPrefix));

      return results;
    }

    private SearchResults ProcessCrashlogLine(string text, bool filterPrefix)
    {
      return Search(text, true, filterPrefix);
    }

    #endregion

    #region IEntitySearcher

    public RenamedBase SearchForNewName(EntityName target)
    {
      RenamedClass c;
      if (classesCache.TryGetValue(target.Name, out c))
        return c;
      if (classesCache.TryGetValue(target.FullName, out c))
        return c;

      return null;
    }

    public RenamedBase SearchForOldName(EntityName target)
    {
      RenamedClass c;
      if (classesCache.TryGetValue(target.Name, out c))
        return c;
      if (classesCache.TryGetValue(target.FullName, out c))
        return c;

      return null;
    }

    public bool HaveSystemEntities
    {
      get { return haveSystemEntities; }
    }

    #endregion

    public long LoadTime
    {
      get { return loadTime; }
    }

    #region Statistics

    public int TotalMethodsCount
    {
      get { return methodsCount; }
    }

    public int TotalClassesCount
    {
      get { return classesCount; }
    }

    public int TotalSubclassesCount
    {
      get { return subclassesCount; }
    }

    public int NamespacesCount
    {
      get { return namespaces.Count; }
    }

    public int ModulesCount
    {
      get { return modules.Count; }
    }

    public int SkippedEntities
    {
      get { return skippedCount; }
    }

    public int ObfuscatedNamespacesCount
    {
      get { return namespacesObfuscated.Count; }
    }

    #endregion
  }
}
