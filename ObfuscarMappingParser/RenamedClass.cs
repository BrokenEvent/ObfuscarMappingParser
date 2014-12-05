using System;
using System.Collections.Generic;
using BrokenEvent.NanoXml;

namespace ObfuscarMappingParser
{
  class RenamedClass: RenamedBase
  {
    private readonly List<RenamedBase> items = new List<RenamedBase>();
    private string ownerClassName;
    private RenamedClass ownerClass;
    private string skipReason;

    public RenamedClass(NanoXmlElement el, Mapping owner)
    {
      if (string.Compare(el.Name, "skippedClass", StringComparison.Ordinal) == 0)
        ParseSkipped(el);
      else
        ParseClass(el, owner);
    }

    private void ParseSkipped(NanoXmlElement el)
    {
      try
      {
        string str = el.GetAttribute("name");

        int j = str.IndexOf('/');
        if (j != -1)
        {
          ownerClassName = str.Substring(0, j);
          name = new Renamed(str.Substring(j + 1));
        }
        else
          name = new Renamed(str);

        skipReason = el.GetAttribute("reason");
      }
      catch (Exception e)
      {
        throw new ObfuscarParserException("Failed to process element", e, el.Path);
      }
    }

    private void ParseClass(NanoXmlElement el, Mapping owner)
    {
      try
      {
        string newName = el.GetAttribute("newName");
        int j = newName.IndexOf('/');
        if (j != -1)
          newName = newName.Substring(j + 1);

        string str = el.GetAttribute("oldName");

        j = str.IndexOf('/');
        if (j != -1)
        {
          ownerClassName = str.Substring(0, j);
          name = new Renamed(str.Substring(j + 1), newName);
        }
        else
          name = new Renamed(str, newName);
      }
      catch (Exception e)
      {
        throw new ObfuscarParserException("Failed to process class element: ", e, el.Path);
      }

      foreach (NanoXmlElement element in el.ChildElements)
      {
        if (!element.Name.StartsWith("renamed"))
          continue;

        items.Add(new RenamedItem(element, this));
      }     
    }

    public string SkipReason
    {
      get { return skipReason; }
    }

    public string OwnerClassName
    {
      get { return ownerClassName; }
    }

    public RenamedClass OwnerClass
    {
      get { return ownerClass; }
      set { ownerClass = value; }
    }

    public List<RenamedBase> Items
    {
      get { return items; }
    }

    public string TransformNamespace
    {
      get
      {
        if (ownerClass != null)
          return ownerClass.TransformNamespace;

        if (!name.HaveNewName)
          return name.NameOld.Namespace;

        return string.Format("{0} → {1}", name.NameOld.Namespace, name.NameNew.Namespace);
      }
    }

    public override EntityType EntityType
    {
      get { return EntityType.Class; }
    }

    public IEnumerable<RenamedBase> Search(string[] values, int index)
    {
      if (!name.HaveNewName) // unable to search, no new name
        yield break;

      if (!name.NameNew.CompareNamespace(values, ref index)) // namespace check
        yield break;

      if (values.Length == 0) // no namespace, unable to search
        yield break;

      if (string.Compare(values[index], name.NameNew.Name, StringComparison.Ordinal) != 0) // correct namespace, wrong class name
        yield break;

      index++; // skip own name

      if (index >= values.Length) // last value is own name, so this is the target
      {
        yield return this;
        yield break;
      }

      if (index == values.Length - 1) // the last index, so we're searching in this class
      {
        foreach (RenamedBase item in items)
        {
          if (item.NameNew == null)
            continue; // unable to search, no new name

          if (string.Compare(values[index], item.Name.NameNew.Name, StringComparison.Ordinal) == 0)
            yield return item;
        }
        yield break;
      }

      foreach (RenamedBase item in items)
      {
        if (item.EntityType != EntityType.Class)
          continue; // only search subclasses

        foreach (RenamedBase i in ((RenamedClass)item).Search(values, index))
          yield return i;
      }
    }

    public IEnumerable<RenamedBase> SearchOriginal(string[] values, int index)
    {
      if (!name.NameOld.CompareNamespace(values, ref index)) // namespace check
        yield break;

      if (values.Length == 0) // no namespace, unable to search
        yield break;

      if (string.Compare(values[index], name.NameOld.Name, StringComparison.Ordinal) != 0) // correct namespace, wrong class name
        yield break;

      index++; // skip own name

      if (index >= values.Length) // last value is own name, so this is the target
      {
        yield return this;
        yield break;
      }

      if (index == values.Length - 1) // the last index, so we're searching in this class
      {
        foreach (RenamedBase item in items)
        {
          if (item.NameNew == null)
            continue; // unable to search, no new name

          if (string.Compare(values[index], item.Name.NameOld.Name, StringComparison.Ordinal) == 0)
            yield return item;
        }
        yield break;
      }

      foreach (RenamedBase item in items)
      {
        if (item.EntityType != EntityType.Class)
          continue; // only search subclasses

        foreach (RenamedBase i in ((RenamedClass)item).SearchOriginal(values, index))
          yield return i;
      }
    }

    public RenamedClass SearchForOldName(EntityName target)
    {
      if (target.Compare(NameOld))
        return this;
      if (target.Compare(NameOldFull))
        return this;

      foreach (RenamedBase item in items)
      {
        if (item.EntityType != EntityType.Class)
          continue;

        RenamedClass c = (RenamedClass)item;
        c = c.SearchForOldName(target);
        if (c != null)
          return c;
      }

      return null;
    }

    public RenamedClass SearchForNewName(EntityName target)
    {
      if (name.NameNew != null)
      {
        if (target.Compare(NameNew))
          return this;
        if (target.Compare(NameNewFull))
          return this;
      }

      foreach (RenamedBase item in items)
      {
        if (item.EntityType != EntityType.Class)
          continue;

        RenamedClass c = (RenamedClass)item;
        c = c.SearchForNewName(target);
        if (c != null)
          return c;
      }

      return null;
    }

    public override void UpdateNewNames(IEntitySearcher searcher)
    {
      foreach (RenamedBase item in items)
        item.UpdateNewNames(searcher);
    }

    public override string ModuleOld
    {
      get
      {
        if (ownerClass != null)
          return ownerClass.ModuleOld;
        return base.ModuleOld;
      }
    }

    public override string ModuleNew
    {
      get
      {
        if (ownerClass != null)
          return ownerClass.ModuleNew;
        return base.ModuleNew;
      }
    }

    public override string NameOld
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        if (ownerClass != null)
          return ownerClass.NameOld + "." + name.NameOld.Name;
        return base.NameOld;
      }
    }

    public override string NameNew
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        if (ownerClass != null)
          return ownerClass.NameNew + "." + name.NameNew.Name;
        return base.NameNew;
      }
    }

    public override string NameOldFull
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        if (ownerClass != null)
          return ownerClass.NameOldFull + "." + name.NameOld.Name;
        return base.NameOldFull;
      }
    }

    public override string NameOldPlain
    {
      get { return NameOld; }
    }

    public override string NameNewFull
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        if (ownerClass != null)
          return ownerClass.NameNewFull + "." + name.NameNew.Name;
        return base.NameNewFull;
      }
    }

    public override string NameNewPlain
    {
      get { return NameNew; }
    }

    public int MethodsCount
    {
      get
      {
        int result = 0;
        foreach (RenamedBase item in items)
          if (item.EntityType == EntityType.Method)
            result++;

        return result;
      }
    }

    public override void PurgeTreeNodes()
    {
      base.PurgeTreeNodes();

      foreach (RenamedBase item in items)
        item.PurgeTreeNodes();
    }

    public override IEnumerable<RenamedBase> GetChildItems()
    {
      yield return this;
      foreach (RenamedBase item in items)
        foreach (RenamedBase childItem in item.GetChildItems())
          yield return childItem;
    }
  }
}
