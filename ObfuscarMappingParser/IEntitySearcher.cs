using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser
{
  interface IEntitySearcher
  {
    RenamedBase SearchForNewName(EntityName nameNew);
    RenamedBase SearchForOldName(EntityName nameOld);

    bool HaveSystemEntities { get; }
  }
}
