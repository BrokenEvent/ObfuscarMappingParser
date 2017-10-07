namespace ObfuscarMappingParser
{
  class RenamedParam: Renamed
  {
    protected string modifier;

    public RenamedParam(string nameOld)
    {
      int modifierIndex = nameOld.Length - 1;
      while (nameOld[modifierIndex] == '&' || nameOld[modifierIndex] == '*')
        modifierIndex--;

      modifierIndex++; // return to first mod's index

      if (modifierIndex < nameOld.Length)
      {
        modifier = nameOld.Substring(modifierIndex);
        nameOld = nameOld.Substring(0, modifierIndex);
      }

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
