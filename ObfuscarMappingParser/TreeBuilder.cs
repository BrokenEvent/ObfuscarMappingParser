using System;
using System.Collections.Generic;
using System.Text;

using BrokenEvent.Shared.Controls;

using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser
{
  class TreeBuilder: IEqualityComparer<string>
  {
    private PineappleTreeView tree;
    private MappingWrapper mapping;
    private bool groupNamespaces;
    private bool showModules;
    private bool groupModules;
    private readonly MainForm mainForm;

    private struct NamingValue
    {
      public string module;
      public string oldNs;
      public string newNs;
      public PineappleTreeNode node;

      public NamingValue(string module, string oldNs, string newNs, PineappleTreeNode node)
      {
        this.module = module;
        this.oldNs = oldNs;
        this.newNs = newNs;
        this.node = node;
      }
    }

    private class ModuleData
    {
      public readonly PineappleTreeNode moduleNode;
      public readonly PineappleTreeNode noNsNode;

      public ModuleData(PineappleTreeNode moduleNode, PineappleTreeNode noNsNode)
      {
        this.moduleNode = moduleNode;
        this.noNsNode = noNsNode;
      }
    }

    private List<NamingValue> naming = new List<NamingValue>();
    private Dictionary<string, ModuleData> moduleNamespaces;
    private PineappleTreeNode noNsNode;

    public TreeBuilder(PineappleTreeView tree, MappingWrapper mapping, MainForm mainForm)
    {
      this.tree = tree;
      this.mapping = mapping;
      this.mainForm = mainForm;
    }

    public bool GroupNamespaces
    {
      get { return groupNamespaces; }
      set { groupNamespaces = value; }
    }

    public bool ShowModules
    {
      get { return showModules; }
      set { showModules = value; }
    }

    public bool GroupModules
    {
      get { return groupModules; }
      set { groupModules = value; }
    }

    public void Build()
    {
      tree.Nodes.Clear();

      if (groupModules)
        moduleNamespaces = new Dictionary<string, ModuleData>();
      else
      {
        noNsNode = new PineappleTreeNode("<no namespace>");
        noNsNode.ImageIndex = mainForm.ICON_NO_NAMESPACE;
      }

      foreach (RenamedClass c in mapping.Mapping.Classes)
        BuildClass(c);

      if (groupModules)
      {
        foreach (KeyValuePair<string, ModuleData> ns in moduleNamespaces)
        {
          if (ns.Value.noNsNode.Nodes.Count > 0)
            ns.Value.moduleNode.Nodes.Add(ns.Value.noNsNode);
        }
      }
      else
        if (noNsNode.Nodes.Count > 0)
          tree.Nodes.Add(noNsNode);
    }

    private static PineappleTreeNode CreateNode(string name)
    {
      return new PineappleTreeNode(name);
    }

    private void BuildClass(RenamedClass c)
    {
      PineappleTreeNode classNode = CreateNode(c.NameOld);
      PineappleTreeNode owner = GetRootNodeForClass(c);
      owner.Nodes.Add(classNode);
      BuildClassContent(c, classNode);
    }

    private void BuildSubclass(RenamedClass c, PineappleTreeNode ownerNode)
    {
      PineappleTreeNode classNode = CreateNode(c.NameOld);
      ownerNode.Nodes.Add(classNode);
      BuildClassContent(c, classNode);    
    }
  
    private void BuildClassContent(RenamedClass c, PineappleTreeNode classNode)
    {
      classNode.ImageIndex = mainForm.ICON_CLASS;
      classNode.Tag = c;

      classNode.ToolTipText = BuildHintForClass(c);
      mapping.MapRenamed(c, classNode);

      string subItemText = null;
      if (c.NameNew != null)
      {
        if (c.Name.NameNew.Equals(c.Name.NameOld))
          classNode.HighlightColorIndex = (int)Highlights.NotRenamed;
        else
          subItemText = CheckIfUnicode(showModules ? c.NameNewFull : c.NameNew);
      }
      else
      {
        subItemText = "n/a";
        classNode.HighlightColorIndex = (int)Highlights.NoNewName;
      }

      if (subItemText != null)
      {
        if (Configs.Instance.UseColumns)
          classNode.Subitems.Add(new PineappleTreeSubitem(subItemText));
        else
          classNode.Text += "\n" + subItemText;
      }

      foreach (RenamedBase renamedItem in c.Items)
      {
        if (renamedItem.EntityType == EntityType.Class)
        {
          BuildSubclass((RenamedClass)renamedItem, classNode);
          continue;
        }

        RenamedItem item = (RenamedItem)renamedItem;

        PineappleTreeNode node = new PineappleTreeNode(item.NameOld);
        classNode.Nodes.Add(node);
        node.ImageIndex = GetIconForEntity(item.EntityType, mainForm);
        node.Tag = item;

        mapping.MapRenamed(item, node);

        node.ToolTipText = BuildHintForItem(item);
        subItemText = null;

        if (item.NameNew == null)
        {
          node.HighlightColorIndex = (int)Highlights.NoNewName;
          subItemText = "n/a";
        }
        else
        {
          if (item.NameNew.Equals(item.NameOld))
            node.HighlightColorIndex = (int)Highlights.NotRenamed;
          else
            subItemText = CheckIfUnicode(item.NameNew);
        }

        if (subItemText != null)
        {
          if (Configs.Instance.UseColumns)
            node.Subitems.Add(new PineappleTreeSubitem(subItemText));
          else
            node.Text += "\n" + subItemText;
        }
      }
    }

    public static string BuildHintForClass(RenamedClass c)
    {
      if (c.SkipReason != null)
        return string.Format("Name: {0}\nSkip reason: {1}", c.NameOldFull, c.SkipReason);

      StringBuilder sb = new StringBuilder();
      sb.Append("Old name: ");
      sb.AppendLine(c.NameOldFull);
      sb.Append("New name: ");
      sb.AppendLine(CheckIfUnicode(c.NameNewFull));
      sb.Append("Name transform: ");
      sb.AppendLine(c.TransformName);
      if (c.OwnerClass != null)
      {
        sb.Append("Owner class: ");
        sb.AppendLine(CheckIfUnicode(c.OwnerClass.TransformNameFull));
      }
      else
      {
        sb.Append("Namespace transform: ");
        sb.AppendLine(CheckIfUnicode(c.TransformNamespace));
      }
      sb.Append("Subitems: ");
      sb.AppendLine(c.Items.Count.ToString());
      return sb.ToString();
    }

    public static string BuildHintForItem(RenamedItem item)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Type: ");
      sb.AppendLine(item.EntityType.ToString());
      sb.AppendLine("Old name:");
      sb.AppendLine(item.NameOldSimple);
      sb.AppendLine("New name:");
      sb.AppendLine(CheckIfUnicode(item.NameNewSimple));
      sb.AppendLine("Owner class:");
      sb.AppendLine(CheckIfUnicode(item.Owner.TransformNameFull));
      return sb.ToString();
    }

    private ModuleData GetModuleData(string moduleName)
    {
      ModuleData data;
      if (moduleNamespaces.TryGetValue(moduleName, out data))
        return data;

      PineappleTreeNode moduleNode = new PineappleTreeNode(moduleName);
      moduleNode.ImageIndex = mainForm.ICON_ASSEMBLY;
      tree.Nodes.Add(moduleNode);

      PineappleTreeNode moduleNoNsNode = new PineappleTreeNode("<no namespace>");
      moduleNoNsNode.ImageIndex = mainForm.ICON_NO_NAMESPACE;

      data = new ModuleData(moduleNode, moduleNoNsNode);
      moduleNamespaces.Add(moduleName, data);
      return data;
    }

    private PineappleTreeNode GetNoNsNodeByModule(string moduleName)
    {
      if (!groupModules)
        return noNsNode;

      return GetModuleData(moduleName).noNsNode;
    }

    private PineappleTreeNode GetRootNodeForClass(RenamedClass c)
    {
      if (c.Name.NameOld.Namespace == null)
        return GetNoNsNodeByModule(c.Name.NameNew.Module);

      foreach (NamingValue value in naming)
      {
        if (groupModules && !Equals(value.module, c.ModuleNew))
          continue;

        if (!groupNamespaces && !Equals(value.newNs, c.Name.NameNew.Namespace))
          continue;

        if (!Equals(value.oldNs, c.Name.NameOld.Namespace))
          continue;

        return value.node;
      }

      PineappleTreeNode node = CreateNode(c.Name.NameOld.Namespace);
      node.ImageIndex = mainForm.ICON_NAMESPACE;
      string subItemText = null;
      if (c.Name.NameNew == null)
      {
        node.HighlightColorIndex = (int)Highlights.NoNewName;
        subItemText = "n/a";
      } else if (c.Name.NameNew.CompareNamespace(c.Name.NameOld))
        node.HighlightColorIndex = (int)Highlights.NotRenamed;
      else
        subItemText = c.Name.NameNew.Namespace;

      if (subItemText != null)
      {
        if (Configs.Instance.UseColumns)
          node.Subitems.Add(new PineappleTreeSubitem(subItemText));
        else
          node.Text += "\n" + subItemText;
      }

      if (groupModules)
        GetModuleData(c.Name.NameNew.Module).moduleNode.Nodes.Add(node);
      else
        tree.Nodes.Add(node);

      naming.Add(new NamingValue(c.Name.NameNew.Module, c.Name.NameOld.Namespace, c.Name.NameNew.Namespace, node));

      return node;
    }

    public static int GetIconForEntity(EntityType type, MainForm mainForm)
    {
      switch (type)
      {
        case EntityType.Class:
          return mainForm.ICON_CLASS;
        case EntityType.Event:
          return mainForm.ICON_EVENT;
        case EntityType.Method:
          return mainForm.ICON_METHOD;
        case EntityType.Field:
          return mainForm.ICON_FIELD;
        case EntityType.Property:
          return mainForm.ICON_PROPERTY;
        case EntityType.Constructor:
          return mainForm.ICON_CTOR;
        default:
          return -1;
      }
    }

    private static string CheckIfUnicode(string str)
    {
      if (Configs.Instance.ShowUnicode)
        return str;

      bool containsUnicode = false;
      for (int i = 0; i < str.Length; i++)
        if (str[i] > 255 && str[i] != '→')
        {
          containsUnicode = true;
          break;
        }

      if (!containsUnicode)
        return str;

      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < str.Length; i++)
        if (str[i] > 255 && str[i] != '→')
        {
          sb.Append("\\u");
          sb.Append(((int)str[i]).ToString("X4"));
        }
        else
          sb.Append(str[i]);

      return sb.ToString();
    }

    private enum Highlights
    {
      NoNewName,
      NotRenamed,
    }

    public bool Equals(string x, string y)
    {
      if (x == null || y == null)
        return false;
      if (x.Length != y.Length)
        return false;
      return string.Compare(x, y, StringComparison.Ordinal) == 0;
    }

    public int GetHashCode(string obj)
    {
      return obj.GetHashCode();
    }
  }
}
