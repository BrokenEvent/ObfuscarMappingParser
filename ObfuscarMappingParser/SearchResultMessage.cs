using System.Reflection;

namespace ObfuscarMappingParser
{
  [Obfuscation(Exclude = true)]
  enum SearchResultMessage: uint
  {
    Normal,
    Ambiguous,
    Substitution,
    Failed,
  }
}
