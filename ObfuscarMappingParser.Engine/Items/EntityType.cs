using System.Reflection;

namespace ObfuscarMappingParser
{
  [Obfuscation(Exclude = true)]
  public enum EntityType
  {
    Constructor,
    Method,
    Property,
    Field,
    Class,
    Event,
  }
}
