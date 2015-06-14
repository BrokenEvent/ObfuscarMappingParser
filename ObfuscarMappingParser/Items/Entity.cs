using System;
using System.Collections.Generic;
using System.Text;

namespace ObfuscarMappingParser
{
  class Entity: INamedEntity
  {
    private List<EntityName> methodParams;
    private EntityName result;
    private EntityName name;

    public Entity(string s)
    {
      methodParams = BuildParams(ref s);
      s = s.TrimEnd(' ');
      int i = s.IndexOf(' ');
      if (i == -1)
        name = s;
      else
      {
        string res = s.Substring(0, i);
        if (res != "void")
          result = res;
        name = s.Substring(i).TrimStart(' ');
      }
    }

    public Entity(Entity entity, RenamedBase item)
    {
      methodParams = new List<EntityName>(entity.methodParams);
      result = entity.result;
      name = item.NameOldFull + "." + entity.name.Name;
    }

    public Entity(Entity entity, RenamedBase item, IList<EntityName> methodParams)
    {
      this.methodParams = new List<EntityName>(methodParams);
      result = entity.result;
      name = item.NameOldFull + "." + entity.name.Name;
    }

    private static List<EntityName> BuildParams(ref string s)
    {
      int i = s.IndexOf('(');
      if (i == -1)
        return null;

      List<EntityName> result = new List<EntityName>();
      int paramStart = i;
      i++; // skip (

      int j = s.IndexOf(')');
      if (j != -1)
        j = j - i;
      else
        j = s.Length - i;
      if (j != 0)
      {
        string[] p = s.Substring(i, j).Split(',');
        foreach (string s1 in p)
        {
          string s2 = s1.Trim(' ');
          int k = s2.IndexOf(' ');
          result.Add(k == -1 ? s2 : s2.Substring(0, k));
        }
      }

      s = s.Substring(0, paramStart);
      return result;
    }

    public EntityName Name
    {
      get { return name; }
    }

    public IList<EntityName> MethodParams
    {
      get { return methodParams; }
    }

    public static implicit operator Entity(string s)
    {
      return new Entity(s);
    }

    #region INamedEntity

    public EntityName EntityName
    {
      get { return name; }
    }

    public EntityType EntityType
    {
      get { return methodParams != null ?
        (string.Compare(name.Name, "ctor", StringComparison.Ordinal) == 0 ? EntityType.Constructor : EntityType.Method) :
        (result == null ? EntityType.Class : EntityType.Field); }
    }

    public EntityName EntityResultType
    {
      get { return result; }
    }

    public string Module
    {
      get { return name.Module; }
    }

    private void CreateParams(bool isShort, StringBuilder sb)
    {
      if (methodParams == null)
        return;

      sb.Append('(');
      bool paramAdded = false;
      foreach (EntityName param in methodParams)
      {
        if (paramAdded)
          sb.Append(", ");
        sb.Append(SystemTypeProcessor.SimplifyType(param, !isShort));
        paramAdded = true;
      }

      sb.Append(')');
    }

    private string CreateName(string ownName, bool isShort)
    {
      StringBuilder sb = new StringBuilder();
      if (result != null)
      {
        sb.Append(SystemTypeProcessor.SimplifyType(result, !isShort));
        sb.Append(' ');
      }
      else if (methodParams != null && string.Compare(name.Name, "ctor", StringComparison.Ordinal) != 0) // method with no result - void
        sb.Append("void ");

      sb.Append(ownName);
      CreateParams(isShort, sb);
      return sb.ToString();
    }

    public string NameShort
    {
      get { return CreateName(name.Name, true); }
    }

    public string NameSimple
    {
      get { return CreateName(name.PathName, true); }
    }

    public string NameFull
    {
      get { return CreateName(name.PathName, false); }
    }

    public int MethodParamsCount
    {
      get { return methodParams == null ? 0 : methodParams.Count; }
    }

    public EntityName GetMethodParam(int index)
    {
      return methodParams[index];
    }

    #endregion
  }
}
