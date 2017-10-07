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

    public string AddModifier(string target)
    {
      if (this.modifier == null)
        return target;

      string modifier = this.modifier;
      if (Configs.Instance.SimplifyRef)
      {
        int i = modifier.IndexOf('&');
        if (i != -1)
        {
          target = "ref " + target;
          if (i == modifier.Length - 1)
            modifier = modifier.Substring(0, i);
          else
            modifier = modifier.Substring(0, i) + modifier.Substring(i + 1);
        }
      }

      return target + modifier;
    }

    public bool IsRef
    {
      get { return modifier != null && modifier.IndexOf('&') != -1; }
    }

    public override string ToString()
    {
      if (nameNew == null || nameNew.Compare(nameOld, false))
        return AddModifier(nameOld.PathName);

      return string.Format("{0} → {1}", AddModifier(nameOld.PathName), AddModifier(nameNew.PathName));
    }
  }
}
