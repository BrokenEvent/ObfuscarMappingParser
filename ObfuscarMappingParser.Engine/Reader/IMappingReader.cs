using System.Collections.Generic;

namespace ObfuscarMappingParser.Engine.Reader
{
  public interface IMappingReader
  {
    void Load();

    IEnumerable<IMappingEntity> Entities { get; }
  }
}
