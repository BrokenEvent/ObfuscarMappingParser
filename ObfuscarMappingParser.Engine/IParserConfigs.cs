namespace ObfuscarMappingParser
{
  public interface IParserConfigs
  {
    bool SimplifyNullable { get; }

    bool SimplifyRef { get; }

    bool SimplifySystemNames { get; }
  }
}
