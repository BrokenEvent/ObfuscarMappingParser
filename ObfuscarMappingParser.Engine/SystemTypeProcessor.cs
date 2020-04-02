using System.Collections.Generic;

using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser.Engine
{
  static class SystemTypeProcessor
  {
    // http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx
    private static Dictionary<string, string> replacements = new Dictionary<string, string>
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
      // fallback for case which should never happen
      if (value == null)
        return "??";

      if (ParserConfigs.Instance.SimplifySystemNames && (string.IsNullOrEmpty(value.Namespace) || value.Namespace == "System"))
      {
        string result;
        if (replacements.TryGetValue(value.Name, out result))
          return result;
      }

      return fullName ? value.PathName : value.Name;
    }
  }
}
