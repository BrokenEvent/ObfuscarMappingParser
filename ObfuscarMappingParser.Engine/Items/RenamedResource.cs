using ObfuscarMappingParser.Engine.Reader;

namespace ObfuscarMappingParser.Engine.Items
{
  public class RenamedResource
  {
    private readonly string nameOld;
    private readonly string nameNew;
    private readonly string skipReason;

    public RenamedResource(IMappingEntity reader)
    {
      nameOld = reader.Name;
      nameNew = reader.NewName;
      skipReason = reader.SkipReason;
    }

    public string SkipReason
    {
      get { return skipReason; }
    }

    public string NameOld
    {
      get { return nameOld; }
    }

    public string NameNew
    {
      get { return nameNew; }
    }
  }
}
