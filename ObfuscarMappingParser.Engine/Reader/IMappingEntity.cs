using System.Collections.Generic;

using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser.Engine.Reader
{
  public interface IMappingEntity
  {
    /// <summary>
    /// Gets the name of the entity.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the new name of the entity. Can be null.
    /// </summary>
    string NewName { get; }

    /// <summary>
    /// Gets the skip reason of the entity. May be null if it not skipped.
    /// </summary>
    string SkipReason { get; }

    /// <summary>
    /// Gets the entity type.
    /// </summary>
    EntityType Type { get; }

    /// <summary>
    /// Gets the subentities enumeration.
    /// </summary>
    IEnumerable<IMappingEntity> SubEntities { get; }

    /// <summary>
    /// Gets the path, used in exceptions.
    /// </summary>
    string Path { get; }
  }
}
