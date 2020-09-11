using System.Collections.Generic;
using System.IO;

using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser.Engine.Reader
{
  class TxtMappingReader: IMappingReader
  {
    private readonly string filename;
    private Dictionary<string, TxtSection> sections = new Dictionary<string, TxtSection>();

    public TxtMappingReader(string filename)
    {
      this.filename = filename;
    }

    public void Load()
    {
      string[] lines = File.ReadAllLines(filename);

      int index = 0;
      TxtSection section = null;

      while (index < lines.Length)
      {
        string line = lines[index];

        // empty line
        if (string.IsNullOrWhiteSpace(line))
        {
          index++;
          continue;
        }

        if (char.IsWhiteSpace(line, 0))
          throw new ObfuscarParserException("Section name can't start with whitespace", $"Line {index + 1}: {line}");

        if (line[0] == '[')
        {
          if (section == null)
            throw new ObfuscarParserException("Class out of section", $"Line {index + 1}: {line}");
          ParseClass(lines, ref index, section);
          continue;
        }

        int i = line.IndexOf(':');
        if (i == -1)
          throw new ObfuscarParserException("Section name not ends with :", $"Line {index + 1}: {line}");

        section = new TxtSection(line.Substring(0, i));
        sections.Add(section.Name, section);
        index++;
      }
    }

    private static void ParseClass(string[] lines, ref int index, TxtSection section)
    {
      TxtClassItem c = new TxtClassItem();
      section.Items.Add(c);
      c.Type = EntityType.Class;

      // parse class itself
      ParseItem(lines[index], c);

      // skip class line
      index++;

      // no class block, just item
      if (lines[index] != "{")
      {
        c.Type = EntityType.Resource;
        return;
      }

      // skip {
      index++;

      while (index < lines.Length)
      {
        string line = lines[index];

        // empty line
        if (string.IsNullOrWhiteSpace(line))
        {
          index++;
          continue;
        }

        // end bracket
        if (line == "}")
        {
          // skip its line
          index++;
          return;
        }

        TxtMemberItem member = new TxtMemberItem();
        member.Path = $"Line {index + 1}: {line}";
        ParseItem(line, member);
        c.Members.Add(member);
        index++;
      }
    }

    private static void ParseItem(string line, TxtMemberItem item)
    {
      const string ARROW = "->";
      const string SKIP_REASON = "skipped:";

      // have new name
      int i = line.IndexOf(ARROW);
      if (i != -1)
      {
        item.Name = line.Substring(0, i).Trim();
        item.NewName = line.Substring(i + ARROW.Length).Trim();
        return;
      }

      // skip reason
      i = line.IndexOf(SKIP_REASON);
      if (i != -1)
      {
        item.Name = line.Substring(0, i).Trim();
        item.SkipReason = line.Substring(i + SKIP_REASON.Length).Trim();
      }
    }

    public IEnumerable<IMappingEntity> Entities
    {
      get
      {
        foreach (TxtSection section in sections.Values)
          foreach (TxtClassItem item in section.Items)
            yield return item;
      }
    }
  }

  class TxtSection
  {
    public TxtSection(string name)
    {
      Name = name;
    }

    public string Name { get; }

    public List<TxtClassItem> Items { get; } = new List<TxtClassItem>();
  }

  class TxtMemberItem: IMappingEntity
  {
    public string Name { get; set; }

    public string NewName { get; set; }

    public string SkipReason { get; set; }

    public EntityType Type { get; set; }

    public virtual IEnumerable<IMappingEntity> SubEntities
    {
      get { yield break; }
    }

    public string Path { get; set; }
  }

  class TxtClassItem: TxtMemberItem
  {
    public List<TxtMemberItem> Members { get; } = new List<TxtMemberItem>();

    public override IEnumerable<IMappingEntity> SubEntities
    {
      get { return Members; }
    }
  }
}
