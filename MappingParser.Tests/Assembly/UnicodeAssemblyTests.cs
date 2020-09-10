using NUnit.Framework;

namespace MappingParser.Tests.Assembly
{
  [TestFixture]
  class UnicodeAssemblyTests: BaseAssemblyTests
  {
    [SetUp]
    public void SetUp()
    {
      PreLoad("unicode", "mapping.xml");
    }
  }
}
