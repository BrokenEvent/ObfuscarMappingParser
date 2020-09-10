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

    public static void AssertResult(Result path, SearchResults results)
    {
      INamedEntity result = results.SingleResult;
      Assert.IsNotNull(result, "Have result");
      Assert.IsTrue(results.IsSingleResult, "Single result");
      Assert.AreEqual(SearchResultMessage.Normal, results.Message, "Result type");
      Assert.AreEqual(path.Path, result.NameFull);

      RenamedBase renamedBase = result as RenamedBase;
      Assert.NotNull(renamedBase, "Is RenamedBase");

      if (path.NonObfuscated)
        Assert.AreEqual(path.Path, renamedBase.NameNewFull); // not obfuscated
      else
        Assert.AreNotEqual(path.Path, renamedBase.NameNewFull); // obfuscated
    }
  }

  class Result
  {
    public string Path { get; }
    public bool NonObfuscated { get; }

    public Result(string path, bool nonObfuscated = false)
    {
      Path = path;
      NonObfuscated = nonObfuscated;
    }
  }
}
