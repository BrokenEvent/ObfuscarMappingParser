using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using BrokenEvent.NanoXml;
using BrokenEvent.Shared;
using BrokenEvent.Shared.Rest;
using BrokenEvent.VisualStudioOpener;

using ObfuscarMappingParser.Engine;

namespace ObfuscarMappingParser
{
  internal class Configs: DefaultConfigs, IParserConfigs
  {
    #region Singletone

    private static Configs instance;

    public static Configs Instance
    {
      get
      {
        if (instance == null)
          instance = new Configs();

        if (ParserConfigs.Instance == null)
          ParserConfigs.Instance = instance;
        return instance;
      }
    }

    private Configs()
    {
#if DEBUG
      base.LoadDefaults();
#endif

      base.Load(GetConfigPath(false));
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

    public const string PROPERTY_EDITOR = "editor";

    #region Saveload

    protected override void Load(NanoXmlElement doc)
    {
      NanoXmlElement recentsEl = doc.GetElement("Recents");
      foreach (NanoXmlElement childElement in recentsEl.ChildElements)
      {
        if (File.Exists(childElement.GetAttribute("filename")))
          recents.Add(new RecentItem(childElement));
      }

      NanoXmlElement settingsEl = doc.GetElement("Settings");
      settingsEl.GetValueIfExists("ShowModules", ref showModules);
      settingsEl.GetValueIfExists("ShowSkipped", ref showSkipped);
      settingsEl.GetValueIfExists("ShowResources", ref showResources);
      settingsEl.GetValueIfExists("GroupNamespaces", ref groupNamespaces);
      settingsEl.GetValueIfExists("GroupModules", ref groupModules);
      settingsEl.GetValueIfExists("UseColumns", ref useColumns);
      settingsEl.GetValueIfExists("ShowOriginal", ref showOriginal);
      settingsEl.GetValueIfExists("ShowUnicode", ref showUnicode);
      settingsEl.GetValueIfExists("SimplifySystem", ref simplifySystemNames);
      settingsEl.GetValueIfExists("SimplifyNullable", ref simplifyNullable);
      settingsEl.GetValueIfExists("SimplifyRef", ref simplifyRef);
      settingsEl.GetValueIfExists("SortingType", ref sortingType);
      settingsEl.GetValueIfExists("Editor", ref editor);
      settingsEl.GetValueIfExists("DoubleClickAction", ref doubleClickAction);
      settingsEl.GetValueIfExists("WatchClipboard", ref watchClipboard);

      if (editor == null)
        editor = VisualStudioDetector.GetHighestVisualStudio().Description;

      commandsElement = doc.GetElement("Actions");

      NanoXmlElement updateEl = doc.GetElement("Update");
      if (updateEl == null)
        updateEl = doc.GetElement("update");
      if (updateEl != null)
        updateHelper.Load(updateEl);
    }

    protected override void Save(NanoXmlElement doc)
    {
      NanoXmlElement recentsEl = doc.AppendChild(new NanoXmlElement("Recents"));
      foreach (RecentItem recent in recents)
        recent.Save(recentsEl.AppendChild(new NanoXmlElement("Item")));

      NanoXmlElement settingsEl = doc.AppendChild(new NanoXmlElement("Settings"));
      settingsEl.AppendElement("ShowModules", showModules);
      settingsEl.AppendElement("ShowSkipped", showSkipped);
      settingsEl.AppendElement("ShowResources", showResources);
      settingsEl.AppendElement("GroupNamespaces", groupNamespaces);
      settingsEl.AppendElement("GroupModules", groupModules);
      settingsEl.AppendElement("UseColumns", useColumns);
      settingsEl.AppendElement("ShowOriginal", showOriginal);
      settingsEl.AppendElement("ShowUnicode", showUnicode);
      settingsEl.AppendElement("SimplifySystem", simplifySystemNames);
      settingsEl.AppendElement("SimplifyNullable", simplifyNullable);
      settingsEl.AppendElement("SimplyfyRef", simplifyRef);
      settingsEl.AppendElement("SortingType", sortingType);
      settingsEl.AppendElement("Editor", editor);
      settingsEl.AppendElement("DoubleClickAction", doubleClickAction);
      settingsEl.AppendElement("WatchClipboard", watchClipboard);

      if (commandsElement != null)
        doc.AppendChild(commandsElement);

      updateHelper.Save(doc.AppendChild(new NanoXmlElement("Update")));
    }

    protected override void LoadDefaults()
    {
      editor = VisualStudioDetector.GetHighestVisualStudio().Description;
    }

    #endregion

    private bool showModules;
    private bool groupNamespaces = true;
    private bool groupModules;
    private SortingTypes sortingType;
    private string editor;
    private bool useColumns = true;
    private bool showOriginal = true;
    private bool showUnicode;
    private bool showResources = true;
    private bool showSkipped;
    private bool simplifySystemNames = true;
    private bool simplifyNullable = true;
    private bool simplifyRef = true;
    private bool watchClipboard = false;
    private DoubleClickActions doubleClickAction = DoubleClickActions.OpenInEditor;
    private NanoXmlElement commandsElement;
    private UpdateHelper updateHelper = new UpdateHelper { CheckInterval = UpdateHelper.UpdateCheckInterval.TenDays };

    [Obfuscation(Exclude = true)]
    public enum SortingTypes
    {
      OriginalNameAscending,
      OriginalNameDescending,
      NewNameAscending,
      NewNameDescending,
    }

    [Obfuscation(Exclude = true)]
    public enum DoubleClickActions
    {
      [Description("Copy Old Name")]
      CopyOldName = Actions.CopyOldName,
      [Description("Copy Full Old Name")]
      CopyFullOldName = Actions.CopyFullOldName,
      [Description("Copy New Name")]
      CopyNewName = Actions.CopyNewName,
      [Description("Copy Full New Name")]
      CopyFullNewName = Actions.CopyFullNewName,
      [Description("Open in Editor")]
      OpenInEditor = Actions.OpenInEditor
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

    public bool ShowResources
    {
      get { return showResources; }
      set { showResources = value; }
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

    public bool ShowSkipped
    {
      get { return showSkipped; }
      set { showSkipped = value; }
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

    public bool SimplifyRef
    {
      get { return simplifyRef; }
      set { simplifyRef = value; }
    }

    public bool WatchClipboard
    {
      get { return watchClipboard; }
      set { watchClipboard = value; }
    }

    public DoubleClickActions DoubleClickAction
    {
      get { return doubleClickAction; }
      set { doubleClickAction = value; }
    }

    public NanoXmlElement CommandsElement
    {
      get { return commandsElement; }
      set { commandsElement = value; }
    }

    public UpdateHelper UpdateHelper
    {
      get { return updateHelper; }
    }

    public string Editor
    {
      get { return editor; }
      set { editor = value; }
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
  }
}
