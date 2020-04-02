using System.Reflection;

namespace ObfuscarMappingParser.Engine
{
  [Obfuscation(Exclude = true)]
  public enum SearchResultMessage: uint
  {
    Normal,
    Ambiguous,
    Substitution,
    Failed,
  }
}
