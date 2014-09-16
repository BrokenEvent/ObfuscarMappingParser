using System;
using System.Collections.Generic;
using System.Text;
using BrokenEvent.Shared.TreeView;

namespace ObfuscarMappingParser
{
  class TreeBuilder :IEqualityComparer<string>
  {
    private PineappleTreeView tree;
    private Mapping mapping;
    private bool groupNamespaces;
    private bool showModules;
    private bool groupModules;

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

    public TreeBuilder(PineappleTreeView tree, Mapping mapping)
    {
      this.tree = tree;
      this.mapping = mapping;
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
        noNsNode.ImageIndex = (int)Icons.NoNs;
      }

      foreach (RenamedClass c in mapping.Classes)
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
      return Configs.Instance.UseColumns ? new PineappleTreeNode(name) : new PineappleTreeNodeMultiline(name);
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
      classNode.ImageIndex = (int)Icons.Class;
      classNode.Tag = c;

      classNode.ToolTipText = BuildHintForClass(c);
      c.TreeNode = classNode;
      if (c.NameNew != null)
      {
        classNode.Subitems.Add(showModules ? c.NameNewFull : c.NameNew);
        if (c.Name.NameNew.Equals(c.Name.NameOld))
          classNode.HighlightColorIndex = (int)Highlights.NotRenamed;
      }
      else
      {
        classNode.Subitems.Add("n/a");
        classNode.HighlightColorIndex = (int)Highlights.NoNewName;
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
        node.ImageIndex = GetIconForEntity(item.EntityType);
        node.Tag = item;
        item.TreeNode = node;

        node.ToolTipText = BuildHintForItem(item);

        if (item.NameNew == null)
        {
          node.HighlightColorIndex = (int)Highlights.NoNewName;
          node.Subitems.Add("n/a");
        }
        else
        {
          if (item.NameNew.Equals(item.NameOld))
            node.HighlightColorIndex = (int)Highlights.NotRenamed;
          node.Subitems.Add(item.NameNew);
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
      sb.AppendLine(c.NameNewFull);
      sb.Append("Name transform: ");
      sb.AppendLine(c.TransformName);
      if (c.OwnerClass != null)
      {
        sb.Append("Owner class: ");
        sb.AppendLine(c.OwnerClass.TransformNameFull);
      }
      else
      {
        sb.Append("Namespace transform: ");
        sb.AppendLine(c.TransformNamespace);        
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
      sb.AppendLine(item.NameNewSimple);
      sb.AppendLine("Owner class:");
      sb.AppendLine(item.Owner.TransformNameFull);
      return sb.ToString();
    }

    private ModuleData GetModuleData(string moduleName)
    {
      ModuleData data;
      if (moduleNamespaces.TryGetValue(moduleName, out data))
        return data;

      PineappleTreeNode moduleNode = new PineappleTreeNode(moduleName);
      moduleNode.ImageIndex = (int)Icons.Module;
      tree.Nodes.Add(moduleNode);

      PineappleTreeNode moduleNoNsNode = new PineappleTreeNode("<no namespace>");
      moduleNoNsNode.ImageIndex = (int)Icons.NoNs;

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
      node.ImageIndex = (int)Icons.Ns;
      if (c.Name.NameNew == null)
      {
        node.HighlightColorIndex = (int)Highlights.NoNewName;
        node.Subitems.Add("n/a");
      } else if (c.Name.NameNew.CompareNamespace(c.Name.NameOld))
      {
        node.HighlightColorIndex = (int)Highlights.NotRenamed;
        node.Subitems.Add(c.Name.NameNew.Namespace);
      }
      else
        node.Subitems.Add(c.Name.NameNew.Namespace);

      if (groupModules)
        GetModuleData(c.Name.NameNew.Module).moduleNode.Nodes.Add(node);
      else
        tree.Nodes.Add(node);

      naming.Add(new NamingValue(c.Name.NameNew.Module, c.Name.NameOld.Namespace, c.Name.NameNew.Namespace, node));

      return node;
    }

    public static int GetIconForEntity(EntityType type)
    {
      switch (type)
      {
        case EntityType.Class:
          return (int)Icons.Class;
        case EntityType.Event:
          return (int)Icons.Event;
        case EntityType.Method:
          return (int)Icons.Method;
        case EntityType.Field:
          return (int)Icons.Field;
        case EntityType.Property:
          return (int)Icons.Property;
        default:
          return -1;
      }
    }

    public enum Icons: int
    {
      Ns,
      NoNs,
      Class,
      Event,
      Method,
      Field,
      Property,
      Module
    }

    private enum Highlights: int
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
