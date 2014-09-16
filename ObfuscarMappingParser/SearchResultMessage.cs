using System.Reflection;

namespace ObfuscarMappingParser
{
  [Obfuscation(Exclude = true)]
  enum SearchResultMessage: uint
  {
    Normal,
    Ambigous,
    Substitution,
    Failed,
  }
}
