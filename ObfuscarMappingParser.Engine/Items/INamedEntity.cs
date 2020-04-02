namespace ObfuscarMappingParser
{
  public interface INamedEntity
  {
    EntityName EntityName { get; }
    EntityType EntityType { get; }
    EntityName EntityResultType { get; }
    string Module { get; }
    string NameShort { get; }
    string NameSimple { get; }
    string NameFull { get; }
    int MethodParamsCount { get; }
    EntityName GetMethodParam(int index);
  }
}
