using NUnit.Framework;

namespace MappingParser.Tests.Assembly
{
  [TestFixture]
  class KoreanAssemblyTests : BaseAssemblyTests
  {
    [SetUp]
    public void SetUp()
    {
      PreLoad("korean", "mapping.xml");
    }
  }
}
