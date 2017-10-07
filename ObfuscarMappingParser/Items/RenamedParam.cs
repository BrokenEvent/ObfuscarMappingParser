namespace ObfuscarMappingParser
{
  class RenamedParam: Renamed
  {
    protected string modifier;

    public RenamedParam(string nameOld)
    {
      // if can be only &/ref ?
      if (nameOld.EndsWith("&"))
        modifier = "&";
      else if (nameOld.EndsWith("*"))
        modifier = "*";

      if (modifier != null)
        nameOld = nameOld.Substring(0, nameOld.Length - 1);

      this.nameOld = new EntityName(nameOld);
      nameNew = (EntityName)this.nameOld.Clone();
    }

    public string Modifier
    {
      get { return modifier; }
    }

    protected string TryAddModifier(string target)
    {
      if (modifier == null)
        return target;
      return target + modifier;
    }

    public override string ToString()
    {
      if (nameNew == null || nameNew.Compare(nameOld, false))
        return TryAddModifier(nameOld.PathName);

      return string.Format("{0} → {1}", TryAddModifier(nameOld.PathName), TryAddModifier(nameNew.PathName));
    }
  }
}
