using NUnit.Framework;

namespace MappingParser.Tests.Assembly
{
  [TestFixture]
  class NormalTxtAssemblyTests: BaseAssemblyTests
  {
    [SetUp]
    public void SetUp()
    {
      PreLoad("normal_txt", "mapping.txt");
    }
  }
}
