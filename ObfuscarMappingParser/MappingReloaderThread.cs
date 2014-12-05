using System;
using System.Threading;

namespace ObfuscarMappingParser
{
  class MappingReloaderThread
  {
    private Mapping mapping;
    private LoadingThreadCompleted loadingThreadCompleted;

    public delegate void LoadingThreadCompleted(object mapping, string filename);

    public MappingReloaderThread(Mapping mapping)
    {
      this.mapping = mapping;
    }

    public void Start(LoadingThreadCompleted callback)
    {
      loadingThreadCompleted = callback;
      Thread thread = new Thread(Start);
      thread.Start();
    }

    private void Start()
    {
      try
      {
        mapping.Reload();
        if (loadingThreadCompleted != null)
          loadingThreadCompleted(mapping, mapping.Filename);
      }
      catch (Exception e)
      {
        if (loadingThreadCompleted != null)
          loadingThreadCompleted(e, mapping.Filename);
      }
    }
  }
}
