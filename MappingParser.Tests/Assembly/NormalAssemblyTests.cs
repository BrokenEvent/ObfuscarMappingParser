using NUnit.Framework;

namespace MappingParser.Tests.Assembly
{
  [TestFixture]
  class NormalAssemblyTests: BaseAssemblyTests
  {
    [SetUp]
    public void SetUp()
    {
      PreLoad("normal", "mapping.xml");
    }
  }
}
