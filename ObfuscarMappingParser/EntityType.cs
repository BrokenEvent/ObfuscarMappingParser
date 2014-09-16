using System.Reflection;

namespace ObfuscarMappingParser
{
  [Obfuscation(Exclude = true)]
  enum EntityType
  {
    Method,
    Property,
    Field,
    Class,
    Event,
  }
}
