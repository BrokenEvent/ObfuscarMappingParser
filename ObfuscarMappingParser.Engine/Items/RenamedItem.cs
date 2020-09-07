using System;
using System.Collections.Generic;
using System.Text;

using BrokenEvent.NanoXml;

namespace ObfuscarMappingParser.Engine.Items
{
  public class RenamedItem: RenamedBase
  {
    private EntityType entityType;
    private RenamedParam resultType;
    private RenamedClass owner;
    private List<RenamedParam> methodParams;

    public RenamedItem(NanoXmlElement el, RenamedClass owner)
    {
      try
      {
        this.owner = owner;
        entityType = (EntityType)Enum.Parse(typeof (EntityType), el.Name.Substring(7));
        string str = el.GetAttribute("oldName");

        int i;
        if ((i = str.IndexOf(' ')) != -1)
        {
          string s = str.Substring(0, i);
          int k = s.IndexOf('/');
          if (k != -1)
            resultType = new RenamedParam(s.Substring(0, k) + "." + s.Substring(k + 1));
          else
            resultType = new RenamedParam(s);
          str = str.Substring(i + 1);
        }

        if ((i = str.IndexOf("::")) != -1)
          str = str.Substring(i + 2);

        if ((i = str.IndexOf('(')) != -1)
        {
          methodParams = new List<RenamedParam>();
          foreach (string s in EntityName.ParseList(str, i + 1, ')'))
          {
            int k = s.IndexOf('/');
            if (k != -1)
              methodParams.Add(new RenamedParam(s.Substring(0, k) + "." + s.Substring(k + 1)));
            else
              methodParams.Add(new RenamedParam(s));
          }

          str = str.Substring(0, i);

          i = str.IndexOf('[');
          if (i != -1 && str[i + 2] == ']')
            str = str.Substring(0, i);
        }

        string strNew = el.GetAttribute("newName");
        if (strNew != "dropped")
          name = new Renamed(str, strNew);
        else
          name = new Renamed(str);
      }
      catch (Exception e)
      {
        throw new ObfuscarParserException("Failed to process item element", e, el.Path);
      }
    }

    public override EntityType EntityType
    {
      get { return entityType; }
    }

    public Renamed ResultType
    {
      get { return resultType; }
    }

    public List<RenamedParam> MethodParams
    {
      get { return methodParams; }
    }

    public string MethodParamList(bool isShort, bool useNew)
    {
      if (methodParams.Count == 0)
        return string.Empty;

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < methodParams.Count; i++)
      {
        string paramValue;
        if (useNew)
          paramValue = SystemTypeProcessor.SimplifyType(methodParams[i].NameNew, !isShort);
        else
          paramValue = SystemTypeProcessor.SimplifyType(methodParams[i].NameOld, !isShort);

        sb.Append(paramValue);

        if (i < methodParams.Count - 1)
          sb.Append(", ");
      }

      return sb.ToString();
    }

    public RenamedClass Owner
    {
      get { return owner; }
    }

    private string BuildName(string className, string ownName, bool useNew, bool isShort)
    {
      if (className == null)
        className = string.Empty;
      else
        className = className + ".";

      string result;
      if (resultType == null)
        result = "void";
      else
      {
        EntityName resultName = useNew ? resultType.NameNew : resultType.NameOld;
        result = SystemTypeProcessor.SimplifyType(resultName, !isShort);
      }

      if (entityType == EntityType.Method)
        return string.Format("{0} {1}{2}({3})", result, className, ownName, MethodParamList(isShort, useNew));

      if (resultType != null)
        return string.Format("{0} {1}{2}", result, className, ownName);

      return string.Format("{0}{1}", className, ownName);
    }

    public override string NameOld
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        return BuildName(null, name.NameOld.Name, false, true);
      }
    }

    public override string NameNew
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        return BuildName(null, name.NameNew.Name, true, true);
      }
    }

    public override string NameOldSimple
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        return BuildName(owner.NameOld, name.NameOld.PathName, false, true);
      }
    }

    public override string NameNewSimple
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        return BuildName(owner.NameNew, name.NameNew.PathName, true, true);
      }
    }

    public override string NameOldFull
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        return BuildName(owner.NameOld, name.NameOld.PathName, false, false);
      }
    }

    public override string NameNewFull
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        return BuildName(owner.NameNew, name.NameNew.PathName, true, false);
      }
    }

    public override string NameOldPlain
    {
      get
      {
        if (name.NameOld == null)
          return UNKNOWN_NAME;

        return owner.NameOld + "." + name.NameOld.Name;
      }
    }

    public override string NameNewPlain
    {
      get
      {
        if (name.NameNew == null)
          return UNKNOWN_NAME;

        return owner.NameNew + "." + name.NameNew.Name;
      }
    }

    public override string ModuleOld
    {
      get { return owner.ModuleOld; }
    }

    public override string ModuleNew
    {
      get { return owner.ModuleNew; }
    }

    public bool Compare(string name)
    {
      int i = name.IndexOf('(');

      EntityName ename = new EntityName(name.Substring(0, i));
      if (!ename.Compare(this.name.NameNew, false))
        return false;

      int j = name.IndexOf(')', i);
      string p = name.Substring(i + 1, j - i - 1);
      string[] strings = p.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
      if (strings.Length != methodParams.Count)
        return false;

      for (int k = 0; k < strings.Length; k++)
      {
        string s = strings[k].Trim(' ');
        if (s == "*")
          continue;

        EntityName e = new EntityName(s);
        if (!e.Compare(methodParams[k].NameNew, true))
          return false;
      }

      return true;
    }

    public bool CompareParams(IList<EntityName> compareTo)
    {
      if (methodParams.Count != compareTo.Count)
        return false;

      for (int i = 0; i < methodParams.Count; i++)
      {
        if (!methodParams[i].NameNew.Compare(compareTo[i], true))
          return false;
      }

      return true;
    }

    public override void UpdateNewNames(IEntitySearcher searcher)
    {
      if (resultType != null)
        resultType.UpdateNewName(searcher);

      if (methodParams != null)
        foreach (RenamedParam param in methodParams)
          param.UpdateNewName(searcher);
    }

    #region INamesEntity overrides

    public override EntityName EntityResultType
    {
      get { return resultType == null ? null : resultType.NameOld; }
    }

    public override int MethodParamsCount
    {
      get { return methodParams == null ? 0 : methodParams.Count; }
    }

    public override EntityName GetMethodParam(int index)
    {
      return methodParams[index].NameOld;
    }

    #endregion
  }
}
