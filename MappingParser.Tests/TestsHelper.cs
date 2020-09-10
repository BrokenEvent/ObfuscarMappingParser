using System.IO;
using System.Text;

using NUnit.Framework;

using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;

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

    public static void AssertResult(string methodPath, SearchResults results)
    {
      INamedEntity result = results.SingleResult;
      Assert.IsNotNull(result, "Have result");
      Assert.IsTrue(results.IsSingleResult, "Single result");
      Assert.AreEqual(SearchResultMessage.Normal, results.Message, "Result type");
      Assert.AreEqual(methodPath, result.NameFull);
    }
  }
}
