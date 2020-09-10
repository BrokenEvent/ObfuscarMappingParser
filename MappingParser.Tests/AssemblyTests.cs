using System;
using System.Collections.Generic;

using NUnit.Framework;

using ObfuscarMappingParser.Engine;

using TestAssembly;

namespace MappingParser.Tests
{
  [TestFixture]
  class AssemblyTests
  {
    private Mapping mapping;

    [SetUp]
    public void SetUp()
    {
      ParserConfigs.Instance = new ParserConfigsImpl();
      mapping = new Mapping(TestHelper.TranslatePath("Mapping.xml"));
    }

    private void DoTest(Action action, params Result[] expectedMethods)
    {
      string stackTrace = null;

      try
      {
        action();
        Assert.Fail("Test action didn't fail.");
      }
      catch (Exception e)
      {
        stackTrace = e.StackTrace;
      }

      // sanity
      Assert.NotNull(stackTrace, "stackTrace != null");

      List<SearchResults> results = mapping.ProcessCrashlog(stackTrace);

      for (int i = 0; i < expectedMethods.Length; i++)
        TestHelper.AssertResult(expectedMethods[i], results[i]);
    }

    [Test]
    public void PublicCrash()
    {
      DoTest(
          () => new PublicClass1().PublicCrash(),
          new Result("void TestAssembly.PublicClass1.PublicCrash()", true)
        );
    }

    [Test]
    public void PrivateCrash()
    {
      DoTest(
          () => new PublicClass1().PrivateCrash(),
          new Result("void TestAssembly.PrivateClass1.Crash()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrash()", true)
        );
    }

    [Test]
    public void PrivateCrashDeeper()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashDeeper(),
          new Result("void TestAssembly.PrivateClass1.Crash()"),
          new Result("void TestAssembly.PrivateClass1.CrashDeeper()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashDeeper()", true)
        );
    }

    [Test]
    public void PrivateCrashSubclass()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclass(),
          new Result("void TestAssembly.PrivateClass1.SubClass.Crash()"),
          new Result("void TestAssembly.PrivateClass1.CrashSubclass()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashSubclass()", true)
        );
    }

    [Test]
    public void PrivateCrashSubclassDeeper()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclassDeeper(),
          new Result("void TestAssembly.PrivateClass1.SubClass.Crash()"),
          new Result("void TestAssembly.PrivateClass1.SubClass.CrashDeeper()"),
          new Result("void TestAssembly.PrivateClass1.CrashSubclassDeeper()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashSubclassDeeper()", true)
        );
    }

    [Test]
    public void PrivateCrashSubclassParam()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclassParam(),
          new Result("void TestAssembly.PrivateClass1.SubClass.Crash()"),
          new Result("void TestAssembly.PrivateClass1.Crash(TestAssembly.PrivateClass1.SubClass)"),
          new Result("void TestAssembly.PrivateClass1.CrashSubclassParam()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashSubclassParam()", true)
        );
    }
  }
}
