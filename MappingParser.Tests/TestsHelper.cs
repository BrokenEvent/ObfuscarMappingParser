using System.IO;
using System.Text;

using NUnit.Framework;

namespace MappingParser.Tests
{
  static class TestHelper
  {
    public static string ReadAllText(string filename)
    {
      return File.ReadAllText(TranslatePath(filename));
    }

    public static string ReadAllText(string filename, Encoding encoding)
    {
      return File.ReadAllText(TranslatePath(filename), encoding);
    }

    public static string TranslatePath(string filename)
    {
      return Path.Combine(TestContext.CurrentContext.TestDirectory, filename);
    }
  }
}
