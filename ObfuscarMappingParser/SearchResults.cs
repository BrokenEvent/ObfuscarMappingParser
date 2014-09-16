using System.Collections.Generic;

namespace ObfuscarMappingParser
{
  class SearchResults
  {
    private List<INamedEntity> results;
    private INamedEntity resultItem;
    private SearchResultMessage message = SearchResultMessage.Failed;
    private readonly string original;
    private Entity source;

    public SearchResults(string original, Entity source, IEnumerable<RenamedBase> items)
    {
      this.original = original;
      this.source = source;
      foreach (RenamedBase item in items)
        AddResult(item);
    }

    public SearchResults(string original, Entity source, IEnumerable<INamedEntity> items)
    {
      this.original = original;
      this.source = source;
      foreach (INamedEntity item in items)
      {
        AddResult(item);
        if (item is Entity)
          message = SearchResultMessage.Substitution;
      }
    }

    private void AddResult(INamedEntity item)
    {
      if (results != null)
      {
        results.Add(item);
        return;
      }

      if (resultItem != null)
      {
        results = new List<INamedEntity>();
        results.Add(resultItem);
        results.Add(item);
        resultItem = null;
        message = SearchResultMessage.Ambigous;
        return;
      }

      resultItem = item;
      message = SearchResultMessage.Normal;
    }

    public bool IsSingleResult
    {
      get { return resultItem != null; }
    }

    public bool HasValue
    {
      get { return resultItem != null || results != null; }
    }

    public IList<INamedEntity> Results
    {
      get { return results; }
    }

    public SearchResultMessage Message
    {
      get { return message; }
    }

    public INamedEntity SingleResult
    {
      get { return resultItem ?? (results == null ? null : (results.Count > 0 ? results[0] : null)); }
    }

    public override string ToString()
    {
      return ToString(OutputType.Simple);
    }

    public string Original
    {
      get { return original; }
    }

    public INamedEntity Result
    {
      get
      {
        if (results != null)
          return results[0];
        else
          return resultItem ?? source;
      }
    }

    public string ToString(OutputType outputType)
    {
      INamedEntity result;
      if (results != null)
        result = results[0];
      else
        result = resultItem;

      if (result == null)
        result = source;

      string resultStr = null;
      switch (outputType)
      {
        case OutputType.Short:
          resultStr = result.NameShort;
          break;
        case OutputType.Simple:
          resultStr = result.NameSimple;
          break;
        case OutputType.Full:
          resultStr = result.NameFull;
          break;
      }

      if (message == SearchResultMessage.Ambigous)
        resultStr += "/* ambigous */";
      return resultStr;
    }
  }

  public enum OutputType
  {
    Short,
    Simple,
    Full
  }
}
