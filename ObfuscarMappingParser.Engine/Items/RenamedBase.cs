using System.Collections.Generic;

namespace ObfuscarMappingParser
{
  public abstract class RenamedBase: INamedEntity
  {
    protected Renamed name;

    public const string UNKNOWN_NAME = "n/a";

    public Renamed Name
    {
      get { return name; }
    }

    public virtual string NameOld
    {
      get { return name.NameOld == null ? UNKNOWN_NAME : name.NameOld.PathName; }
    }

    public virtual string NameNew
    {
      get { return name.NameNew == null ? UNKNOWN_NAME : name.NameNew.PathName; }
    }

    public virtual string NameOldSimple
    {
      get { return NameOld; }
    }

    public virtual string NameNewSimple
    {
      get { return NameNew; }
    }

    public virtual string NameOldFull
    {
      get { return name.NameOld == null ? UNKNOWN_NAME : name.NameOld.FullName; }
    }

    public virtual string NameNewFull
    {
      get { return name.NameNew == null ? UNKNOWN_NAME : name.NameNew.FullName; }
    }

    public abstract string NameOldPlain { get; }

    public abstract string NameNewPlain { get; }

    public virtual string TransformName
    {
      get { return string.Format("{0} → {1}", NameOld, NameNew); }
    }

    public virtual string TransformSimple
    {
      get { return string.Format("{0} → {1}", NameOldSimple, NameNewSimple); }
    }

    public virtual string TransformNameFull
    {
      get { return string.Format("{0} → {1}", NameOldFull, NameNewFull); }
    }

    public virtual string ModuleOld
    {
      get { return name.NameOld != null ? name.NameOld.Module : UNKNOWN_NAME; }
    }

    public virtual string ModuleNew
    {
      get { return name.NameNew != null ? name.NameNew.Module : UNKNOWN_NAME; }
    }

    public override string ToString()
    {
      return TransformSimple;
    }

    public abstract void UpdateNewNames(IEntitySearcher searcher);

    public virtual IEnumerable<RenamedBase> GetChildItems()
    {
      yield return this;
    }


    #region INamedEntity

    public EntityName EntityName
    {
      get { return name.NameOld; }
    }

    public abstract EntityType EntityType { get; }

    public virtual EntityName EntityResultType
    {
      get { return null; }
    }

    public virtual string Module
    {
      get { return ModuleOld; }
    }

    public virtual string NameShort
    {
      get { return NameOld; }
    }

    public virtual string NameSimple
    {
      get { return NameOldSimple; }
    }

    public virtual string NameFull
    {
      get { return NameOldFull; }
    }

    public virtual int MethodParamsCount
    {
      get { return 0; }
    }

    public virtual EntityName GetMethodParam(int index)
    {
      return null;
    }

    #endregion
  }
}
