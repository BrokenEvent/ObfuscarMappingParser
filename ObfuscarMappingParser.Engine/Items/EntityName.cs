using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ObfuscarMappingParser
{
  public class EntityName: ICloneable, IEquatable<EntityName>
  {
    private string @namespace;
    private string[] nsCache;
    private string name;
    private string module;
    private string modifier;
    private EntityName[] genericParams;

    internal static IEnumerable<string> ParseList(string str, int index1, char endChar)
    {
      if (str[index1] == ')')
        yield break;

      int i = index1;
      int tokenStart = index1;

      do
      {
        if (str[i] == '<')
        {
          i++; // skip <
          int braces = 1;
          while (braces > 0 && i < str.Length)
          {
            if (str[i] == '>')
              braces--;
            else if (str[i] == '<')
              braces++;
            i++;
          }
        }

        if (str[i] == endChar)
        {
          yield return str.Substring(tokenStart, i - tokenStart);
          break;
        }

        if (str[i] == ',')
        {
          yield return str.Substring(tokenStart, i - tokenStart);
          i++; // skip ,
          while (str[i] == ' ')
            i++;
          tokenStart = i;
          continue;
        }

        i++;
      }
      while (true);
    }

    public EntityName(string value)
    {
      if (value == string.Empty)
      {
        name = value;
        return;
      }

      if (value[0] == '[')
      {
        int i = value.IndexOf(']');
        module = value.Substring(1, i - 1);
        value = value.Substring(i + 1);
      }

      int j = value.IndexOf('<');
      if (j != -1 && value[value.Length - 1] == '>')
      {
        List<string> p = new List<string>(ParseList(value, j + 1, '>'));
        value = value.Substring(0, j);
        genericParams = new EntityName[p.Count];
        for (int q = 0; q < p.Count; q++)
          genericParams[q] = new EntityName(p[q]);
      }

      j = value.IndexOf('`');
      if (j != -1)
        value = value.Substring(0, j);

      char delimiter = '\0';
      if (value.IndexOf(':') != -1)
        delimiter = ':';
      else if (value.IndexOf('.') != -1)
        delimiter = '.';

      if (delimiter != '\0')
      {
        int i = value.LastIndexOf(delimiter);
        while (i > 0 && value[i - 1] == delimiter)
          i--;
        @namespace = value.Substring(0, i);        
        nsCache = @namespace.Split('.');
        while (i < value.Length && value[i] == delimiter)
          i++;
        value = value.Substring(i);
      }

      name = value;
    }

    private EntityName(string ns, string[] nsCache, string name, string module, EntityName[] genericParams)
    {
      @namespace = ns;
      if (nsCache != null)
      {
        this.nsCache = new string[nsCache.Length];
        for (int i = 0; i < nsCache.Length; i++)
          this.nsCache[i] = nsCache[i];
      }
      if (genericParams != null)
      {
        this.genericParams = new EntityName[genericParams.Length];
        for (int i = 0; i < genericParams.Length; i++)
          this.genericParams[i] = new EntityName(
              genericParams[i].@namespace,
              genericParams[i].nsCache,
              genericParams[i].name,
              genericParams[i].module,
              genericParams[i].genericParams
            );
      }
      this.name = name;
      this.module = module;
    }

    public void AssignName(EntityName entityName)
    {
      name = entityName.name;
      @namespace = entityName.@namespace;
      if (entityName.nsCache != null)
      {
        nsCache = new string[entityName.nsCache.Length];
        for (int i = 0; i < entityName.nsCache.Length; i++)
          nsCache[i] = entityName.nsCache[i];
      }
      else
        nsCache = null;

      module = entityName.module;
    }

    private string BuildGenericParams(bool full)
    {
      if (genericParams == null)
        return string.Empty;

      StringBuilder sb = new StringBuilder();
      sb.Append('<');
      for (int i = 0; i < genericParams.Length; i++)
      {
        if (i > 0)
          sb.Append(", ");
        sb.Append(SystemTypeProcessor.SimplifyType(genericParams[i], full));
      }
      sb.Append('>');

      return sb.ToString();
    }

    public string Namespace
    {
      get { return @namespace; }
    }

    public string Name
    {
      get
      {
        if (name == "Nullable" && ParserConfigs.Instance.SimplifyNullable)
          return AddModifier(SystemTypeProcessor.SimplifyType(genericParams[0], false) + "?", modifier);

        return AddModifier(name + BuildGenericParams(false), modifier);
      }
    }

    public string Module
    {
      get { return module; }
    }

    public string Modifier
    {
      get { return modifier; }
      set { modifier = value; }
    }

    public string PathName
    {
      get
      {
        if (name == "Nullable" && ParserConfigs.Instance.SimplifyNullable)
          return AddModifier(SystemTypeProcessor.SimplifyType(genericParams[0], true) + "?", modifier);

        return AddModifier(@namespace != null ? @namespace + "." + name + BuildGenericParams(true): name + BuildGenericParams(true), modifier);
      }
    }

    public string FullName
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        if (module != null)
        {
          sb.Append("[");
          sb.Append(module);
          sb.Append("]");
        }
        if (@namespace != null)
        {
          sb.Append(@namespace);
          sb.Append(".");
        }
        sb.Append(name);
        sb.Append(BuildGenericParams(true));
        return sb.ToString();
      }
    }

    public override string ToString()
    {
      return PathName;
    }

    public static implicit operator EntityName(string value)
    {
      if (string.IsNullOrEmpty(value))
        return null;
      return new EntityName(value);
    }

    public static string AddModifier(string target, string modifier)
    {
      if (modifier == null)
        return target;

      if (ParserConfigs.Instance.SimplifyRef)
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

    public bool Compare(EntityName to, bool ignoreNs)
    {
      if (!(ignoreNs && (@namespace == null || to.@namespace == null)) &&
          string.Compare(@namespace, to.@namespace, StringComparison.Ordinal) != 0)
        return false;

      return string.Compare(name, to.name, StringComparison.Ordinal) == 0;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      EntityName target = null;
      if (obj is string)
        target = new EntityName(obj as string);
      if (obj is EntityName)
        target = (EntityName)obj;
      if (target == null)
        return false;

      return Compare(target, false);
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
      unchecked
      {
        int code = (int)2166136261;
        if (@namespace != null)
          code = (code * 16777619) ^ @namespace.GetHashCode();

        code = (code * 16777619) ^ name.GetHashCode();
        return code;
      }
    }

    public object Clone()
    {
      return new EntityName(@namespace, nsCache, name, module, genericParams);
    }

    public bool CompareNamespace(string[] values, ref int start)
    {
      if (nsCache == null || nsCache.Length == 0 || values.Length == 0)
        return true;

      int result = 0;

      while (string.Compare(nsCache[result], values[start], StringComparison.Ordinal) == 0)
      {
        result++;
        start++;

        if (result >= nsCache.Length)
          return true;
        if (start >= values.Length)
          return false;
      }

      return result > 0;
    }

    public bool Compare(string to)
    {
      return Compare(new EntityName(to), false);
    }

    public bool CompareNamespace(EntityName entityName)
    {
      int i = 0;
      if (entityName.nsCache == null)
        return true;
      return CompareNamespace(entityName.nsCache, ref i);
    }

    public void UpdateGenericParams(IEntitySearcher searcher)
    {
      if (genericParams == null)
        return;

      for (int i = 0; i < genericParams.Length; i++)
      {
        if (genericParams[i].@namespace != null &&
            (string.Compare(genericParams[i].nsCache[0], "System", StringComparison.Ordinal) != 0 || searcher.HaveSystemEntities))
        {
          RenamedBase renamedBase = searcher.SearchForOldName(genericParams[i]);
          if (renamedBase != null)
            genericParams[i].AssignName(renamedBase.Name.NameNew);

          genericParams[i].UpdateGenericParams(searcher);
        }
      }
    }

    public bool Equals(EntityName other)
    {
      return Compare(other, false);
    }
  }
}
