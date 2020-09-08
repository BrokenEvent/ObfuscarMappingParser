using System;

namespace ObfuscarMappingParser.Engine.Items
{
  public class Renamed
  {
    protected EntityName nameOld;
    protected EntityName nameNew;

    public EntityName NameOld
    {
      get { return nameOld; }
      internal set { nameOld = value; }
    }

    public EntityName NameNew
    {
      get { return nameNew; }
    }

    public Renamed(string nameOld, string nameNew = null)
    {
      if (string.IsNullOrWhiteSpace(nameOld))
        throw new ArgumentNullException(nameof(nameOld));

      this.nameOld = new EntityName(nameOld);
      this.nameNew = string.IsNullOrWhiteSpace(nameNew) ? (EntityName)this.nameOld.Clone() : new EntityName(nameNew);
    }

    public Renamed(EntityName nameOld, EntityName nameNew)
    {
      if (nameOld == null)
        throw new ArgumentNullException(nameof(nameOld));

      this.nameOld = nameOld;
      this.nameNew = nameNew;

      if (this.nameNew == null)
        this.nameNew = (EntityName)nameOld.Clone();
    }

    protected Renamed() { }

    public Renamed(Renamed nameOld, EntityName nameNew)
    {
      this.nameNew = nameNew;
      this.nameOld = nameOld.nameOld;
    }

    public bool HaveNewName
    {
      get { return nameNew != null && !nameNew.Compare(nameOld, false); }
    }

    public override string ToString()
    {
      if (nameNew == null || nameNew.Compare(nameOld, false))
        return nameOld.PathName;

      return string.Format("{0} → {1}", nameOld.PathName, nameNew.PathName);
    }

    public void UpdateNewName(IEntitySearcher searcher)
    {
      nameNew.UpdateGenericParams(searcher);

      if (nameNew.Namespace == null)
        return;

      // some optimization
      if (nameNew.Namespace.StartsWith("System") && !searcher.HaveSystemEntities)
        return;

      if (!nameNew.Compare(nameOld, false))
        return;

      RenamedBase r = searcher.SearchForOldName(nameNew);
      if (r != null)
        nameNew.AssignName(new EntityName(r.NameNew));
    }
  }
}
