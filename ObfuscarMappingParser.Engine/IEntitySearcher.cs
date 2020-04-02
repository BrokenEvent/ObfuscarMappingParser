namespace ObfuscarMappingParser
{
  public interface IEntitySearcher
  {
    RenamedBase SearchForNewName(EntityName nameNew);
    RenamedBase SearchForOldName(EntityName nameOld);

    bool HaveSystemEntities { get; }
  }
}
