namespace ObfuscarMappingParser
{
  static class SystemTypeProcessor
  {
    // http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx
    private static string[,] replacements = new string[,]
      {
        {"Boolean", "bool"},
        {"Byte", "byte"},
        {"SByte", "sbyte"},
        {"Char", "char"},
        {"Decimal", "decimal"},
        {"Double", "double"},
        {"Single", "float"},
        {"Int32", "int"},
        {"UInt32", "uint"},
        {"Int64", "long"},
        {"UInt64", "ulong"},
        {"Object", "object"},
        {"Int16", "short"},
        {"UInt16", "ushort"},
        {"String", "string"}
      };

    public static string SimplifyType(EntityName value, bool fullName)
    {
      if (Configs.Instance.SimplifySystemNames && (string.IsNullOrEmpty(value.Namespace) || value.Namespace == "System"))
      {
        string result = TryReplace(value.Name);
        if (result != null)
          return result;
      }

      return fullName ? value.PathName : value.Name;
    }

    private static string TryReplace(string s)
    {
      for (int i = 0; i < replacements.GetLength(0); i++)
        if (s == replacements[i, 0])
          return replacements[i, 1];
      return null;
    }
  }
}
