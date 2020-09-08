using System;
using System.Collections.Generic;

using BrokenEvent.NanoXml;

using ObfuscarMappingParser.Engine.Items;

namespace ObfuscarMappingParser.Engine.Reader
{
  class XmlMappingReader: IMappingReader
  {
    private NanoXmlElement xml;
    private readonly string filename;

    public XmlMappingReader(string filename)
    {
      this.filename = filename;
    }

    public void Load()
    {
      NanoXmlDocument doc = NanoXmlDocument.LoadFromFile(filename);
      xml = doc.DocumentElement;
    }

    public IEnumerable<IMappingEntity> Entities
    {
      get
      {
        NanoXmlElement e;

        e = xml.GetElement("renamedTypes");
        if (e != null)
          foreach (IMappingEntity entity in ParseGroup(e))
            yield return entity;

        e = xml.GetElement("skippedTypes");
        if (e != null)
          foreach (IMappingEntity entity in ParseGroup(e))
            yield return entity;

        // TODO: renamedResources, skippedResources
      }
    }

    private static IEnumerable<IMappingEntity> ParseGroup(NanoXmlElement el)
    {
      foreach (NanoXmlElement e in el.ChildElements)
        yield return new XmlMappingEntity(e);
    }

    private class XmlMappingEntity: IMappingEntity
    {
      private readonly NanoXmlElement el;

      private const string PREFIX_RENAMED = "renamed";
      private const string PREFIX_SKIPPED = "skipped";

      public XmlMappingEntity(NanoXmlElement el)
      {
        this.el = el;
        Name = el.GetAttribute("oldName");
        if (Name == null)
          Name = el.GetAttribute("name");
        if (Name == null)
          throw new ObfuscarParserException("Unable to get name", el.Path);

        NewName = el.GetAttribute("newName");
        if (NewName == null)
        {
          SkipReason = el.GetAttribute("reason");
          if (SkipReason == null)
            throw new ObfuscarParserException("Neither new name, nor skip reason not found", el.Path);
        }

        string typeValue;
        if (el.Name.StartsWith(PREFIX_RENAMED))
          typeValue = el.Name.Substring(PREFIX_RENAMED.Length);
        else if (el.Name.StartsWith(PREFIX_SKIPPED))
          typeValue = el.Name.Substring(PREFIX_SKIPPED.Length);
        else
          throw new ObfuscarParserException($"Invalid or unsupported tag: {el.Name}", el.Path);

        Type = (EntityType)Enum.Parse(typeof(EntityType), typeValue);
      }

      public string Name { get; }
      public string NewName { get; }
      public string SkipReason { get; }
      public EntityType Type { get; }

      public IEnumerable<IMappingEntity> SubEntities
      {
        get
        {
          foreach (NanoXmlElement e in el.ChildElements)
            yield return new XmlMappingEntity(e);
        }
      }

      public string Path
      {
        get { return el.Path; }
      }
    }
  }
}
