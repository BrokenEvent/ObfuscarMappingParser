using System.Reflection;

namespace ObfuscarMappingParser
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
