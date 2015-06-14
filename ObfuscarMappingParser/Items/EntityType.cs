using System.Reflection;

namespace ObfuscarMappingParser
{
  [Obfuscation(Exclude = true)]
  enum EntityType
  {
    Constructor,
    Method,
    Property,
    Field,
    Class,
    Event,
  }
}
