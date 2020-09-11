using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ObfuscarMappingParser.Engine.Reader
{
  public static class FormatFactory
  {
    private static readonly Dictionary<string, FormatDescriptor> formats = new Dictionary<string, FormatDescriptor>();

    private static void RegisterFormat(FormatDescriptor desc)
    {
      formats.Add(desc.Extension, desc);
    }

    static FormatFactory()
    {
      RegisterFormat(new FormatDescriptor("XML Mapping", ".xml", s => new XmlMappingReader(s)));
      RegisterFormat(new FormatDescriptor("TXT Mapping", ".txt", s => new TxtMappingReader(s)));
    }

    /// <summary>
    /// Creates a mapping reader for given filename.
    /// </summary>
    /// <param name="filename">Name of file to create reader for.</param>
    /// <returns>Mapping reader for given file.</returns>
    public static IMappingReader CreateReader(string filename)
    {
      string ext = Path.GetExtension(filename).ToLower();

      FormatDescriptor desc;
      if (!formats.TryGetValue(ext, out desc))
        throw new ObfuscarParserException("Unable to get reader for file extension", ext);

      return desc.ReaderCreator(filename);
    }

    /// <summary>
    /// Gets the enumeration of all supported extensions.
    /// </summary>
    public static IEnumerable<string> SupportedExtensions
    {
      get
      {
        foreach (FormatDescriptor value in formats.Values)
          yield return value.Extension;
      }
    }

    /// <summary>
    /// Checks whether the file format can be opened with one of existing readers.
    /// </summary>
    /// <param name="filename">Name of file to check.</param>
    /// <returns>True if there is a reader for given filename and false otherwise.</returns>
    public static bool IsOpenable(string filename)
    {
      string ext = Path.GetExtension(filename).ToLower();

      return formats.ContainsKey(ext);
    }

    /// <summary>
    /// Checks whether there is a reader for given file extension.
    /// </summary>
    /// <param name="ext">Extension to check. <c>.ext</c></param>
    /// <returns>True if there is a reader for given extension and false otherwise.</returns>
    public static bool HasExtension(string ext)
    {
      return formats.ContainsKey(ext);
    }

    /// <summary>
    /// Builds a filter list for open/save dialogs.
    /// </summary>
    /// <returns></returns>
    public static string BuildFilterList()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("All Supported|");

      bool isFirst = true;

      foreach (FormatDescriptor desc in formats.Values)
      {
        if (!isFirst)
          sb.Append(';');
        sb.Append('*').Append(desc.Extension);
        isFirst = false;
      }

      foreach (FormatDescriptor desc in formats.Values)
        sb.Append('|').Append(desc.Name).Append("|*").Append(desc.Extension);

      return sb.ToString();
    }

    private class FormatDescriptor
    {
      public FormatDescriptor(string name, string extension, Func<string, IMappingReader> readerCreator)
      {
        Name = name;
        Extension = extension.ToLower();
        ReaderCreator = readerCreator;
      }

      public string Name { get; }
      public string Extension { get; }
      public Func<string, IMappingReader> ReaderCreator { get; }
    }
  }
}
