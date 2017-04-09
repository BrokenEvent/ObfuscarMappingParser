using System.ComponentModel;

namespace ObfuscarMappingParser
{
  enum Actions
  {
    [Description("Open mapping")]
    [Category("File")]
    OpenFile,

    [Description("Reload mapping")]
    [Category("File")]
    ReloadFile,

    [Description("Attach PDB")]
    [Category("File")]
    AttachPdb,

    [Description("Manage PDBs")]
    [Category("File")]
    ManagePdb,

    [Description("Exit")]
    [Category("UI")]
    Exit,

    [Description("Show Statistics")]
    [Category("UI")]
    Statistics,

    [Description("Show Settings")]
    [Category("UI")]
    Settings,

    [Description("Deobfuscate stacktrace")]
    [Category("Tools")]
    DeobfuscateStacktrace,

    [Description("Analyze stacktrace")]
    [Category("Tools")]
    AnalyzeStacktrace,

    [Description("Search for new name")]
    [Category("Tools")]
    Search,

    [Description("Search for original name")]
    [Category("Tools")]
    SearchForOriginal,

    [Description("Convert RAW data")]
    [Category("Tools")]
    Convert,

    [Description("About")]
    [Category("UI")]
    About,

    [Description("Copy old name")]
    [Category("Context actions")]
    CopyOldName,
    [Description("Copy full old name")]
    [Category("Context actions")]
    CopyFullOldName,
    [Description("Copy new name")]
    [Category("Context actions")]
    CopyNewName,
    [Description("Copy full new name")]
    [Category("Context actions")]
    CopyFullNewName,
    [Description("Open in Editor")]
    [Category("Context actions")]
    OpenInEditor
  }
}
