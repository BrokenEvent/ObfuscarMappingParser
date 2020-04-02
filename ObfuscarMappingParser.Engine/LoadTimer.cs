using System.Diagnostics;

namespace ObfuscarMappingParser
{
  public struct LoadTimer
  {
    private Stopwatch stopwatch;
    private string operationName;

    public LoadTimer(string operationName): this()
    {
      this.operationName = operationName;
      stopwatch = Stopwatch.StartNew();
    }

    public long Stop()
    {
      stopwatch.Stop();
      Debug.WriteLine("{0}: {1} ms", operationName, stopwatch.ElapsedMilliseconds);
      return stopwatch.ElapsedMilliseconds;
    }
  }
}
