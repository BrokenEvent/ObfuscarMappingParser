using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BrokenEvent.NanoXml;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  internal class Configs: DefaultConfigs
  {
    #region Singletone

    private static Configs instance;

    public static Configs Instance
    {
      get
      {
        if (instance == null)
          instance = new Configs();
        return instance;
      }
    }

    private Configs()
    {
#if DEBUG
      LoadDefaults();
#endif

      Load(GetConfigPath(false));
    }

    #endregion

    public override string ApplicationName
    {
      get { return "ObfuscarMappingParser"; }
    }

    public override string ConfigsFilename
    {
      get { return "configs.xml"; }
    }

    private bool showModules;
    private bool groupNamespaces;
    private bool groupModules;
    private SortingTypes sortingType;
    private VSOpener.VisualStudioVersion visualStudioVersion;
    private bool useColumns;
    private bool showOriginal;
    private bool showUnicode;
    private bool simplifySystemNames;
    private bool simplifyNullable;

    [Obfuscation(Exclude = true)]
    public enum SortingTypes
    {
      OriginalNameAscending,
      OriginalNameDescending,
      NewNameAscending,
      NewNameDescending,
    }

    public bool ShowModules
    {
      get { return showModules; }
      set { showModules = value; }
    }

    public bool GroupNamespaces
    {
      get { return groupNamespaces; }
      set { groupNamespaces = value; }
    }

    public bool GroupModules
    {
      get { return groupModules; }
      set { groupModules = value; }
    }

    public bool UseColumns
    {
      get { return useColumns; }
      set { useColumns = value; }
    }

    public bool ShowOriginal
    {
      get { return showOriginal; }
      set { showOriginal = value; }
    }

    public bool ShowUnicode
    {
      get { return showUnicode; }
      set { showUnicode = value; }
    }

    public bool SimplifySystemNames
    {
      get { return simplifySystemNames; }
      set { simplifySystemNames = value; }
    }

    public bool SimplifyNullable
    {
      get { return simplifyNullable; }
      set { simplifyNullable = value; }
    }

    public VSOpener.VisualStudioVersion VisualStudioVersion
    {
      get { return visualStudioVersion; }
      set { visualStudioVersion = value; }
    }

    public SortingTypes SortingType
    {
      get { return sortingType; }
      set { sortingType = value; }
    }

    private class RecentItem
    {
      private string filename;
      private Dictionary<string, List<string>> additionalItems = new Dictionary<string, List<string>>();
      private Dictionary<string, string> properties = new Dictionary<string, string>();

      public RecentItem(string filename)
      {
        this.filename = filename;
      }

      public RecentItem(NanoXmlElement el)
      {
        filename = el.GetAttribute("filename");

        foreach (NanoXmlAttribute attribute in el.Attributes)
          if (attribute.Name != "filename")
            properties.Add(attribute.Name, attribute.Value);

        foreach (NanoXmlElement element in el.ChildElements)
        {
          List<string> list;
          if (!additionalItems.TryGetValue(element.Name, out list))
          {
            list = new List<string>();
            additionalItems.Add(element.Name, list);
          }

          list.Add(element.Value);
        }
      }

      public string Filename
      {
        get { return filename; }
      }

      public IList<string> this[string name]
      {
        get
        {
          List<string> result;
          if (!additionalItems.TryGetValue(name, out result))
          {
            result = new List<string>();
            additionalItems.Add(name, result);
          }
          return result;
        }
      }

      public Dictionary<string, string> Properties
      {
        get { return properties; }
      }

      public void Save(NanoXmlElement el)
      {
        el.AddAttribute("filename", filename);
        foreach (KeyValuePair<string, string> property in properties)
          el.AddAttribute(property.Key, property.Value);

        foreach (KeyValuePair<string, List<string>> item in additionalItems)
          foreach (string s in item.Value)
            el.AppendChild(item.Key, s);
      }
    }

    private List<RecentItem> recents = new List<RecentItem>();

    public void AddRecent(string item)
    {
      RecentItem r = null;
      foreach (RecentItem recent in recents)
        if (string.Compare(recent.Filename, item, StringComparison.OrdinalIgnoreCase) == 0)
        {
          r = recent;
          recents.Remove(r);
          break;
        }

      if (r == null)
        r = new RecentItem(item);
      
      recents.Insert(0, r);
    }

    public IEnumerable<string> Recents
    {
      get
      {
        foreach (RecentItem item in recents)
          yield return item.Filename;
      }
    }

    public void AddRecentPdb(string filename, string pdb)
    {
      AddRecentAdditional(filename, "Pdb", pdb);
    }

    public IList<string> GetRecentPdb(string filename)
    {
      return GetRecentAdditional(filename, "Pdb");
    }

    public void RemoveRecentPdb(string filename, string pdb)
    {
      RemoveRecentAdditional(filename, "Pdb", pdb);
    }

    public void AddRecentAdditional(string filename, string name, string value)
    {
      foreach (RecentItem item in recents)
        if (string.Compare(item.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
        {
          item[name].Add(value);
          break;
        }
    }

    public IList<string> GetRecentAdditional(string filename, string name)
    {
      foreach (RecentItem item in recents)
        if (string.Compare(item.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
          return item[name];

      return null;
    }

    public void RemoveRecentAdditional(string filename, string name, string value)
    {
      foreach (RecentItem item in recents)
        if (string.Compare(item.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
        {
          item[name].Remove(value);
          break;
        }
    }

    public void AddRecentProperty(string filename, string name, string value)
    {
      foreach (RecentItem item in recents)
        if (string.Compare(item.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
        {
          item.Properties[name] = value;
          break;
        }
    }

    public string GetRecentProperty(string filename, string name)
    {
      foreach (RecentItem item in recents)
        if (string.Compare(item.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
        {
          string result;
          item.Properties.TryGetValue(name, out result);
          return result;
        }

      return null;
    }

    public bool HaveRecents
    {
      get { return recents.Count > 0; }
    }

    protected override void Load(NanoXmlElement doc)
    {
      NanoXmlElement recentsEl = (NanoXmlElement)doc["Recents"];
      foreach (NanoXmlElement childElement in recentsEl.ChildElements)
      {
        if (File.Exists(childElement.GetAttribute("filename")))
          recents.Add(new RecentItem(childElement));
      }

      NanoXmlElement settingsEl = (NanoXmlElement)doc["Settings"];
      settingsEl.GetValueIfExists("ShowModules", ref showModules);
      settingsEl.GetValueIfExists("GroupNamespaces", ref groupNamespaces);
      settingsEl.GetValueIfExists("GroupModules", ref groupModules);
      settingsEl.GetValueIfExists("UseColumns", ref useColumns);
      settingsEl.GetValueIfExists("ShowOriginal", ref showOriginal);
      settingsEl.GetValueIfExists("ShowUnicode", ref showUnicode);
      settingsEl.GetValueIfExists("SimplifySystem", ref simplifySystemNames);
      settingsEl.GetValueIfExists("SimplifyNullable", ref simplifyNullable);
      settingsEl.GetValueIfExists("SortingType", ref sortingType);
      settingsEl.GetValueIfExists("VisualStudioVersion", ref visualStudioVersion);
    }

    protected override void Save(NanoXmlElement doc)
    {
      NanoXmlElement recentsEl = doc.AppendChild(new NanoXmlElement("Recents"));
      foreach (RecentItem recent in recents)
        recent.Save(recentsEl.AppendChild(new NanoXmlElement("Item")));

      NanoXmlElement settingsEl = doc.AppendChild(new NanoXmlElement("Settings"));
      settingsEl.AppendChild(new NanoXmlElement("ShowModules", showModules.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("GroupNamespaces", groupNamespaces.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("GroupModules", groupModules.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("UseColumns", useColumns.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("ShowOriginal", showOriginal.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("ShowUnicode", showUnicode.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("SimplifySystem", simplifySystemNames.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("SimplifyNullable", simplifyNullable.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("SortingType", sortingType.ToString()));
      settingsEl.AppendChild(new NanoXmlElement("VisualStudioVersion", visualStudioVersion.ToString()));
    }

    protected override void LoadDefaults()
    {
      showModules = false;
      groupNamespaces = true;
      useColumns = true;
      sortingType = SortingTypes.OriginalNameAscending;
      visualStudioVersion = VSOpener.HighestVersion;
      showOriginal = true;
      showUnicode = false;
      simplifySystemNames = true;
      simplifyNullable = true;
    }
  }
}
