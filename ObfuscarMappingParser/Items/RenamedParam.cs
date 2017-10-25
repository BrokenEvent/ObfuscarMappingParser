namespace ObfuscarMappingParser
{
  class RenamedParam: Renamed
  {
    public RenamedParam(string nameOld)
    {
      int modifierIndex = nameOld.Length - 1;
      do
      {
        if (nameOld[modifierIndex] == '&' || nameOld[modifierIndex] == '*')
          modifierIndex--;
        else if (nameOld[modifierIndex] == ']' || nameOld[modifierIndex - 1] == '[')
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

      this.nameOld = new EntityName(nameOld);
      this.nameOld.Modifier = modifier;
      nameNew = (EntityName)this.nameOld.Clone();
      nameNew.Modifier = modifier;
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
