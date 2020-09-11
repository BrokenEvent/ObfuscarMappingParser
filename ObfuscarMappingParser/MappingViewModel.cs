using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using BrokenEvent.PDBReader;
using BrokenEvent.Shared.Controls;

using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser
{
  class MappingViewModel
  {
    private Dictionary<RenamedBase, PineappleTreeNode> nodesMap = new Dictionary<RenamedBase, PineappleTreeNode>();
    private DateTime lastModified;

    private List<PdbFile> pdbFiles = new List<PdbFile>();

    public MappingViewModel(string filename)
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

    #region PDB

    public List<PdbFile> PdbFiles
    {
      get { return pdbFiles; }
    }

    public bool SearchInPdb(out string filename, out int lineNumber, RenamedBase item)
    {
      filename = null;
      lineNumber = -1;

      string s = item.NameOldPlain;
      int i = s.LastIndexOf('.');
      if (i == -1)
        return false;

      string className = s.Substring(0, i);
      string itemName = s.Substring(i + 1);
      CodeLocation location = null;

      foreach (PdbFile file in pdbFiles)
      {
        location = file.Resolver.FindLocation(className, itemName);
        if (location != null)
          break;
      }

      if (location == null)
        return false;

      filename = location.FileName;
      lineNumber = (int)location.Line;
      return true;
    }

    public bool IsPdbAttached(string filename)
    {
      foreach (PdbFile file in pdbFiles)
        if (file.Filename.Equals(filename, StringComparison.InvariantCultureIgnoreCase))
          return true;

      return false;
    }

    public bool DetectMarkersForVS(out string filename, out int lineNumber, RenamedBase item)
    {
      filename = null;
      lineNumber = -1;

      if (pdbFiles.Count == 0 || item.EntityType != EntityType.Method)
        return false;

      return SearchInPdb(out filename, out lineNumber, item);
    }

    #endregion
  }
}
