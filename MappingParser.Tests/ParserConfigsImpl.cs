using ObfuscarMappingParser.Engine;

namespace MappingParser.Tests
{
  class ParserConfigsImpl: IParserConfigs
  {
    public bool SimplifyNullable { get; set; } = true;

    public bool SimplifyRef { get; set; } = true;

    public bool SimplifySystemNames { get; set; } = true;
  }
}
