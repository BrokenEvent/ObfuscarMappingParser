using System.Collections.Generic;
using System.Windows.Forms;

using BrokenEvent.Shared.Controls;

namespace ObfuscarMappingParser
{
  class MappingWrapper
  {
    private Dictionary<RenamedBase, PineappleTreeNode> nodesMap = new Dictionary<RenamedBase, PineappleTreeNode>();

    public MappingWrapper(string filename)
    {
      Mapping = new Mapping(filename);
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
