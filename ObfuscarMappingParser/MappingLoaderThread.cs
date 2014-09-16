using System;
using System.Threading;

namespace ObfuscarMappingParser
{
  class MappingLoaderThread
  {
    private readonly string filename;
    private Mapping mapping;
    private LoadingThreadCompleted loadingThreadCompleted;

    public delegate void LoadingThreadCompleted(object mapping, string filename);

    public MappingLoaderThread(string filename)
    {
      this.filename = filename;
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
        mapping = new Mapping(filename);
        if (loadingThreadCompleted != null)
          loadingThreadCompleted(mapping, filename);
      }
      catch (Exception e)
      {
        if (loadingThreadCompleted != null)
          loadingThreadCompleted(e, filename);
      }
    }
  }
}
