namespace ObfuscarMappingParser
{
  class RenamedParam: Renamed
  {
    public static EntityName ParseParam(string nameOld)
    {
      int modifierIndex = nameOld.Length - 1;

      do
      {
        if (nameOld[modifierIndex] == '&' || nameOld[modifierIndex] == '*')
          modifierIndex--;
        else if (modifierIndex > 0 && nameOld.Length > 1 && (nameOld[modifierIndex] == ']' || nameOld[modifierIndex - 1] == '['))
          modifierIndex -= 2;
        else
          break;
      }
      while (true);

      modifierIndex++; // return to first mod's index
      string modifier = null;

      if (modifierIndex < nameOld.Length)
      {
        modifier = nameOld.Substring(modifierIndex);
        nameOld = nameOld.Substring(0, modifierIndex);
      }

      EntityName result = new EntityName(nameOld);
      result.Modifier = modifier;
      return result;
    }

    public RenamedParam(string nameOld)
    {
      this.nameOld = ParseParam(nameOld);
      nameNew = (EntityName)this.nameOld.Clone();
      nameNew.Modifier = this.nameOld.Modifier;
    }

    public string Modifier
    {
      get { return nameOld.Modifier; }
    }

    public override string ToString()
    {
      if (nameNew == null || nameNew.Compare(nameOld, false))
        return nameOld.PathName;

      return string.Format("{0} → {1}", nameOld.PathName, nameNew.PathName);
    }
  }
}
