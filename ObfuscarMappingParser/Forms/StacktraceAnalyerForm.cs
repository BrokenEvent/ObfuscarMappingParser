using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrokenEvent.Shared;
using BrokenEvent.Shared.TreeView;

namespace ObfuscarMappingParser
{
  partial class StacktraceAnalyerForm : Form
  {
    private readonly MainForm mainForm;
    private List<SearchResults> results;

    public StacktraceAnalyerForm(MainForm mainForm, string data, string source)
    {
      this.mainForm = mainForm;
      InitializeComponent();
      pineappleTreeView.ImageList = mainForm.IconsList;
      Text = "Stacktrace analyzer: " + source;

      tbtnShowOriginal.Checked = Configs.Instance.ShowOriginal;

      try
      {
        results = mainForm.Mapping.ProcessCrashlog(data);
      }
      catch (Exception e)
      {
        throw new ObfuscarParserException("Failed to process crashlog", e, data);
      }

      BuildTree();

      pineappleTreeView.Backlights.Add(new PineappleTreeHighlight(Color.FromArgb(255, 224, 224))); // unknown
      pineappleTreeView.Backlights.Add(new PineappleTreeHighlight(Color.FromArgb(224, 255, 224))); // substitution
      pineappleTreeView.Backlights.Add(new PineappleTreeHighlight(Color.LightYellow)); // ambiguous
    }

    private enum Highlights
    {
      Unknown,
      Substitution,
      Ambigous
    }

    private string GetEntityName(INamedEntity entity)
    {
      if (tbtnSimple.Checked)
        return entity.NameSimple;
      if (tbtnShort.Checked)
        return entity.NameShort;
      if (tbtnFull.Checked)
        return entity.NameFull;

      throw new ArgumentException("Invalid selected show mode");
    }

    private void BuildSubstitutionNode(PineappleTreeNode node, INamedEntity entity, string originalLine)
    {
      node.BacklightColorIndex = (int)Highlights.Substitution;
      node.Subitems.Add("Class substitution");
      if (originalLine != null && Configs.Instance.ShowOriginal)
        node.Subitems.Add("Original line: " + originalLine);
      node.ToolTipText = "Target method not found in mapping.";
      node.ImageIndex = TreeBuilder.GetIconForEntity(entity.EntityType);
    }

    private void BuildAmbigousNode(PineappleTreeNode node, SearchResults result)
    {
      node.BacklightColorIndex = (int)Highlights.Ambigous;
      node.Subitems.Add("Ambigous. See tree subitems for details.");

      if (Configs.Instance.ShowOriginal)
        node.Subitems.Add("Original line: " + result.Original);

      node.ToolTipText = "Unable to precisely determine method. Set of variants provided.";
      node.ImageIndex = 8;

      foreach (INamedEntity entity in result.Results)
      {
        PineappleTreeNode entityNode = new PineappleTreeNodeMultiline(GetEntityName(entity));
        node.Nodes.Add(entityNode);
        BuildNormalNode(entityNode, entity, null);
      }
    }

    private void BuildNormalNode(PineappleTreeNode node, INamedEntity entity, string originalLine)
    {
      node.ImageIndex = TreeBuilder.GetIconForEntity(entity.EntityType);
      ItemDescriptor descriptor = new ItemDescriptor(entity);
      string tooltip = descriptor.GetToolTip(); 
      node.Tag = descriptor;

      RenamedItem item = entity as RenamedItem;
      if (item != null)
        node.Subitems.Add("Owner class: " + item.Owner.NameOldFull);

      RenamedBase renamedBase = (RenamedBase)entity;
      if (tbtnShort.Checked)
        node.Subitems.Add("New name: " + renamedBase.NameNew);
      if (tbtnSimple.Checked)
        node.Subitems.Add("New name: " + renamedBase.NameNewSimple);
      if (tbtnFull.Checked)
        node.Subitems.Add("New name: " + renamedBase.NameNewFull);

      if (originalLine != null && Configs.Instance.ShowOriginal)
        node.Subitems.Add("Original line: " + originalLine);

      if (!mainForm.HavePdb)
        tooltip += "Unable to map to source code, no PDB files attached.";
      else
      {
        if (entity.EntityType != EntityType.Method)
          tooltip += "Mapping to source code works only for methods.";
        else
        {
          ProcessPdb(descriptor);
          if (descriptor.Filename != null)
          {
            node.Subitems.Add(PathUtils.ShortenPath(descriptor.Filename, 100) + ":" + descriptor.Line);
            tooltip += descriptor.Filename + ":" + descriptor.Line;
          }
          else
            tooltip += "Unable to map to source code.";
        }
      }

      node.ToolTipText = tooltip;
    }

    private void BuildFailedNode(PineappleTreeNode node, INamedEntity entity, string originalLine)
    {
      node.ImageIndex = TreeBuilder.GetIconForEntity(entity.EntityType);
      node.BacklightColorIndex = (int)Highlights.Unknown;
      node.Subitems.Add("Unable to deobfuscate.");
      if (originalLine != null && Configs.Instance.ShowOriginal)
        node.Subitems.Add("Original line: " + originalLine);
      node.ToolTipText = "Unable to deobfuscate method. It is of an unknown or system assembly.";
    }

    private void BuildTree()
    {
      pineappleTreeView.BeginUpdate();
      pineappleTreeView.Nodes.Clear();

      foreach (SearchResults result in results)
      {
        PineappleTreeNode node = new PineappleTreeNodeMultiline(GetEntityName(result.Result));
        pineappleTreeView.Nodes.Add(node);

        switch (result.Message)
        {
          case SearchResultMessage.Normal:
            BuildNormalNode(node, result.Result, result.Original);
            break;
          case SearchResultMessage.Ambiguous:
            BuildAmbigousNode(node, result);
            break;
          case SearchResultMessage.Substitution:
            BuildSubstitutionNode(node, result.Result, result.Original);
            break;
          case SearchResultMessage.Failed:
            BuildFailedNode(node, result.Result, result.Original);
            break;
        }                
      }

      pineappleTreeView.CollapseAll();
      pineappleTreeView.EndUpdate();
    }

    private void ProcessPdb(ItemDescriptor d)
    {
      if (d.Renamed == null)
        return;
      string filename;
      int line;
      if (mainForm.SearchInPdb(out filename, out line, d.Renamed))
      {
        d.Filename = filename;
        d.Line = line;
      }
    }

    private class ItemDescriptor
    {
      private RenamedBase renamed;
      private INamedEntity entity;
      private string filename;
      private int line;

      public ItemDescriptor(INamedEntity entity)
      {
        this.entity = entity;
        renamed = entity as RenamedBase;
      }

      public string GetToolTip()
      {
        RenamedItem item = renamed as RenamedItem;
        if (item != null)
          return TreeBuilder.BuildHintForItem(item);
        RenamedClass @class = renamed as RenamedClass;
        if (@class != null)
          return TreeBuilder.BuildHintForClass(@class);

        return entity.NameFull;
      }

      public RenamedBase Renamed
      {
        get { return renamed; }
      }

      public string Filename
      {
        get { return filename; }
        set { filename = value; }
      }

      public int Line
      {
        get { return line; }
        set { line = value; }
      }
    }

    #region Show modes

    private void tbtnShort_Click(object sender, EventArgs e)
    {
      tbtnSimple.Checked = false;
      tbtnShort.Checked = true;
      tbtnFull.Checked = false;
      BuildTree();
    }

    private void tbtnSimple_Click(object sender, EventArgs e)
    {
      tbtnSimple.Checked = true;
      tbtnShort.Checked = false;
      tbtnFull.Checked = false;
      BuildTree();
    }

    private void tbtnFull_Click(object sender, EventArgs e)
    {
      tbtnSimple.Checked = false;
      tbtnShort.Checked = false;
      tbtnFull.Checked = true;
      BuildTree();
    }

    #endregion

    private void pineappleTreeView_NodeSelect(object sender, NodeSelectEventArgs e)
    {
      ItemDescriptor descriptor = e.Node.Tag as ItemDescriptor;
      tbtnOpenInTree.Enabled = descriptor != null && descriptor.Renamed != null;
      tbtnOpenInVS.Enabled = descriptor != null && descriptor.Filename != null;
    }

    private void tbtnOpenInTree_Click(object sender, EventArgs e)
    {
      if (pineappleTreeView.SelectedNode == null)
        return;
      ItemDescriptor descriptor = pineappleTreeView.SelectedNode.Tag as ItemDescriptor;
      if (descriptor == null || descriptor.Renamed == null)
        return;

      PineappleTreeNode node = descriptor.Renamed.TreeNode;
      node.TreeView.SelectedNode = node;
      node.EnsureVisible();
      Close();
    }

    private void tbtnOpenInVS_Click(object sender, EventArgs e)
    {
      if (pineappleTreeView.SelectedNode == null)
        return;
      ItemDescriptor descriptor = pineappleTreeView.SelectedNode.Tag as ItemDescriptor;
      if (descriptor == null || descriptor.Filename == null)
        return;

      mainForm.OpenInVisualStudio(descriptor.Filename, descriptor.Line);
    }

    private void tbtnShowOriginal_Click(object sender, EventArgs e)
    {
      Configs.Instance.ShowOriginal = tbtnShowOriginal.Checked = !Configs.Instance.ShowOriginal;
      BuildTree();
    }
  }
}
