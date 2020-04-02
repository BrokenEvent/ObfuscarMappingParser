using System.Reflection;

namespace ObfuscarMappingParser.Engine.Items
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
