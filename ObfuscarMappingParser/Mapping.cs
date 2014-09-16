using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BrokenEvent.NanoXml;

namespace ObfuscarMappingParser
{
  internal class Mapping: IEntitySearcher
  {
    private readonly string filename;
    private List<RenamedClass> classes = new List<RenamedClass>();

    private long timingXML;
    private long timingParsing;
    private long timingSubclasses;
    private long timingUpdateNewNames;

    private int methodsCount;
    private int classesCount;
    private int subclassesCount;
    private int skippedCount;

    private List<string> modules = new List<string>();
    private List<string> namespaces = new List<string>();
    private List<string> namespacesObfuscated = new List<string>();

    private bool haveSystemEntities;

    public Mapping(string filename)
    {
      Stopwatch sw = new Stopwatch();
      this.filename = filename;
      sw.Start();
      NanoXmlDocument xml;
      xml = NanoXmlDocument.LoadFromFile(filename);

      timingXML = sw.ElapsedMilliseconds;
      Debug.WriteLine("XML Loading: " + timingXML + " ms");
      sw.Reset();
      sw.Start();

      NanoXmlElement doc = xml.DocumentElement;
      NanoXmlElement types = (NanoXmlElement)doc["renamedTypes"];

      List<RenamedClass> subclasses = new List<RenamedClass>();

      if (types != null)
        foreach (NanoXmlElement element in types.ChildElements)
        {
          if (string.Compare(element.Name, "renamedClass", StringComparison.Ordinal) == 0)
          {
            RenamedClass c = new RenamedClass(element, this);
            classesCount++;
            if (c.OwnerClassName == null)
            {
              classes.Add(c);
              if (c.Name.NameOld != null && c.Name.NameOld.Namespace != null)
                haveSystemEntities |= c.Name.NameOld.Namespace.StartsWith("System.");
            }
            else
              subclasses.Add(c);

            methodsCount += c.MethodsCount;
            if (c.ModuleNew != null && !modules.Contains(c.ModuleNew))
              modules.Add(c.ModuleNew);
            if (c.Name.NameOld != null && !string.IsNullOrEmpty(c.Name.NameOld.Namespace) && !namespaces.Contains(c.Name.NameOld.Namespace))
              namespaces.Add(c.Name.NameOld.Namespace);
            if (c.Name.NameNew != null && !string.IsNullOrEmpty(c.Name.NameNew.Namespace) && !namespacesObfuscated.Contains(c.Name.NameNew.Namespace))
              namespacesObfuscated.Add(c.Name.NameNew.Namespace);
          }
        }

      types = (NanoXmlElement)doc["skippedTypes"];
      if (types != null)
        foreach (NanoXmlElement element in types.ChildElements)
          if (string.Compare(element.Name, "skippedClass", StringComparison.Ordinal) == 0)
          {
            skippedCount++;
            classesCount++;
            RenamedClass c = new RenamedClass(element, this);
            if (c.OwnerClassName == null)
              classes.Add(c);
            else
              subclasses.Add(c);
          }


      timingParsing = sw.ElapsedMilliseconds;
      Debug.WriteLine("Parsing: " + timingParsing + " ms");
      sw.Reset();
      sw.Start();

      foreach (RenamedClass subclass in subclasses)
      {
        RenamedClass c = (RenamedClass)SearchForOldName(subclass.OwnerClassName);
        if (c == null)
          c = (RenamedClass)SearchForNewName(subclass.OwnerClassName);

        if (c != null)
        {
          c.Items.Add(subclass);
          subclass.OwnerClass = c;
          subclassesCount++;
          continue;
        }

        Debug.WriteLine("Failed to find root class: " + subclass.OwnerClassName);
        classes.Add(subclass);
      }

      timingSubclasses = sw.ElapsedMilliseconds;
      Debug.WriteLine("Subclasses processing: " + timingSubclasses + " ms");
      sw.Reset();
      sw.Start();

      foreach (RenamedClass c in classes)
        c.UpdateNewNames(this);

      timingUpdateNewNames = sw.ElapsedMilliseconds;
      Debug.WriteLine("Values updating: " + timingUpdateNewNames + " ms");
      Debug.WriteLine("Total elapsed: " + TimingTotal + " ms");
      sw.Stop();
    }

    public string Filename
    {
      get { return filename; }
    }

    public List<RenamedClass> Classes
    {
      get { return classes; }
    }

    public List<string> Modules
    {
      get { return modules; }
    }

    public List<string> Namespaces
    {
      get { return namespaces; }
    }

    public List<string> NamespacesObfuscated
    {
      get { return namespacesObfuscated; }
    }

    public void PurgeTreeNodes()
    {
      foreach (RenamedClass renamedClass in classes)
        renamedClass.PurgeTreeNodes();
    }

    #region Search

    public SearchResults Search(string s, bool allowSubstitution)
    {
      if (string.IsNullOrEmpty(s))
        return null;

      Entity entity = s;
      if (entity.EntityType != EntityType.Method)
        return new SearchResults(s, entity, SearchItem(entity));

      return new SearchResults(s, entity, SearchMethod(entity, allowSubstitution));
    }

    private IEnumerable<INamedEntity> SearchMethod(Entity entity, bool allowSubstitution)
    {
      bool hasAdded = false;
      foreach (RenamedBase item in SearchItem(entity))
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

      string[] values = entity.Name.Namespace.Split('.');

      foreach (RenamedClass renamedClass in classes)
      {
        foreach (RenamedBase item in renamedClass.Search(values, 0))
          yield return new Entity(entity, item);
      }
    }

    private IEnumerable<RenamedBase> SearchItem(Entity entity)
    {
      string[] values;
      try
      {
        values = entity.Name.PathName.Split('.');
      }
      catch
      {
        yield break;
      }

      foreach (RenamedClass renamedClass in classes)
      {
        foreach (RenamedBase item in renamedClass.Search(values, 0))
          yield return item;
      }
    }

    #endregion

    #region Crashlog processing

    public string ProcessCrashlogText(string text)
    {
      List<SearchResults> results = ProcessCrashlog(text);
      StringBuilder sb = new StringBuilder();
      foreach (SearchResults result in results)
        sb.AppendLine(result.ToString());

      return sb.ToString();
    }

    public List<SearchResults> ProcessCrashlog(string text)
    {
      string[] strings = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
      List<SearchResults> results = new List<SearchResults>();

      foreach (string s in strings)
        results.Add(ProcessCrashlogLine(s));

      return results;
    }

    private SearchResults ProcessCrashlogLine(string text)
    {
      text = text.TrimStart(' ');
      if (text.StartsWith("at "))
        text = text.Substring(3).TrimStart(' ');
      return Search(text, true);
    }

    #endregion

    #region IEntitySearcher

    public RenamedBase SearchForNewName(EntityName target)
    {
      foreach (RenamedClass renamedClass in classes)
      {
        RenamedClass c = renamedClass.SearchForNewName(target);
        if (c != null)
          return c;
      }

      return null;
    }

    public RenamedBase SearchForOldName(EntityName target)
    {
      foreach (RenamedClass renamedClass in classes)
      {
        RenamedClass c = renamedClass.SearchForOldName(target);
        if (c != null)
          return c;
      }

      return null;
    }

    public bool HaveSystemEntities
    {
      get { return haveSystemEntities; }
    }

    #endregion

    #region Timings

    public long TimingXml
    {
      get { return timingXML; }
    }

    public long TimingParsing
    {
      get { return timingParsing; }
    }

    public long TimingSubclasses
    {
      get { return timingSubclasses; }
    }

    public long TimingUpdateNewNames
    {
      get { return timingUpdateNewNames; }
    }

    public long TimingTotal
    {
      get { return timingXML + timingParsing + timingSubclasses + timingUpdateNewNames; }
    }

    #endregion

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
