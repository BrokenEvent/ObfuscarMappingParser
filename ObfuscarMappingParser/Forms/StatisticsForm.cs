﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

using BrokenEvent.Shared.Forms;

using ObfuscarMappingParser.Engine;

namespace ObfuscarMappingParser
{
  partial class StatisticsForm : BaseForm
  {
    public StatisticsForm(Mapping mapping)
    {
      InitializeComponent();
      lblClassesValue.Text = mapping.TotalClassesCount.ToString();
      lblMethodsValue.Text = mapping.TotalMethodsCount.ToString();
      lblSubclassesValue.Text = mapping.TotalSubclassesCount.ToString();
      lblModulesValue.Text = mapping.ModulesCount.ToString();
      lblNsValue.Text = mapping.NamespacesCount.ToString();
      lblNs2Value.Text = mapping.ObfuscatedNamespacesCount.ToString();
      lblSkippedValue.Text = mapping.SkippedEntities.ToString();
      llblFilename.Text = mapping.Filename;
    }

    private void llblFilename_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start("explorer", "/select," + llblFilename.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, "Failed to start Explorer: " + ex.Message, "Failed to start Explorer.");
      }
    }
  }
}
