using System;
using System.IO;

using BrokenEvent.PDBReader;

namespace ObfuscarMappingParser
{
  public class PdbFile
  {
    private string filename;
    private PdbResolver resolver;
    private DateTime changeDate;

    public PdbFile(string filename)
    {
      this.filename = filename;
      
      ReloadFile();
    }

    public string Filename
    {
      get { return filename; }
    }

    public PdbResolver Resolver
    {
      get { return resolver; }
    }

    public bool CheckFileModification()
    {
      try
      {
        return File.GetLastWriteTime(filename) > changeDate;
      }
      catch
      {
        // ignore any IO errors
        return false;
      }
    }

    public void ReloadFile()
    {
      resolver = new PdbResolver(filename);
      changeDate = File.GetLastWriteTime(filename);
    }
  }
}
