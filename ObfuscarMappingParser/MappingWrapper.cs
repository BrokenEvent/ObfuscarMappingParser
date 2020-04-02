using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using BrokenEvent.Shared.Controls;

using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser
{
  class MappingWrapper
  {
    private Dictionary<RenamedBase, PineappleTreeNode> nodesMap = new Dictionary<RenamedBase, PineappleTreeNode>();
    private DateTime lastModified;

    public MappingWrapper(string filename)
    {
      Mapping = new Mapping(filename);
      lastModified = File.GetLastWriteTime(filename);
    }

    public Mapping Mapping { get; }

    public void MapRenamed(RenamedBase renamed, PineappleTreeNode node)
    {
      nodesMap.Add(renamed, node);
    }

    public PineappleTreeNode FindNode(RenamedBase renamed)
    {
      return nodesMap.TryGetValue(renamed, out PineappleTreeNode node) ? node : null;
    }

    public void PurgeTreeNodes()
    {
      nodesMap.Clear();
    }

    public bool CheckModifications()
    {
      try
      {
        DateTime m = lastModified;
        lastModified = File.GetLastWriteTime(Mapping.Filename);
        return lastModified > m;
      }
      catch
      {
        // ignore all IO errors
        return false;
      }
    }

    #region Autocomplete

    public AutoCompleteStringCollection GetNewNamesCollection()
    {
      AutoCompleteStringCollection result = new AutoCompleteStringCollection();
      foreach (RenamedClass renamedClass in Mapping.Classes)
        foreach (RenamedBase item in renamedClass.GetChildItems())
        {
          if (item.Name.NameNew != null)
            result.Add(item.NameNewPlain);
        }

      return result;
    }

    public AutoCompleteStringCollection GetOldNamesCollection()
    {
      AutoCompleteStringCollection result = new AutoCompleteStringCollection();
      foreach (RenamedClass renamedClass in Mapping.Classes)
        foreach (RenamedBase item in renamedClass.GetChildItems())
          if (item.Name.NameOld != null)
            result.Add(item.NameOldPlain);

      return result;
    }

    #endregion
  }
}
