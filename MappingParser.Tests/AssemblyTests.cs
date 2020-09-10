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

    private void DoTest(Action action, params string[] expectedMethods)
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

      for(int i = 0; i < expectedMethods.Length; i++)
      TestHelper.AssertResult(expectedMethods[i], results[i]);
    }

    [Test]
    public void PublicCrash()
    {
      DoTest(
          () => new PublicClass1().PublicCrash(),
          "void TestAssembly.PublicClass1.PublicCrash()"
        );
    }

    [Test]
    public void PrivateCrash()
    {
      DoTest(
          () => new PublicClass1().PrivateCrash(),
          "void TestAssembly.PrivateClass1.Crash()",
          "void TestAssembly.PublicClass1.PrivateCrash()"
        );
    }

    [Test]
    public void PrivateCrashDeeper()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashDeeper(),
          "void TestAssembly.PrivateClass1.Crash()",
          "void TestAssembly.PrivateClass1.CrashDeeper()",
          "void TestAssembly.PublicClass1.PrivateCrashDeeper()"
        );
    }

    [Test]
    public void PrivateCrashSubclass()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclass(),
          "void TestAssembly.PrivateClass1.SubClass.Crash()",
          "void TestAssembly.PrivateClass1.CrashSubclass()",
          "void TestAssembly.PublicClass1.PrivateCrashSubclass()"
        );
    }

    [Test]
    public void PrivateCrashSubclassDeeper()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclassDeeper(),
          "void TestAssembly.PrivateClass1.SubClass.Crash()",
          "void TestAssembly.PrivateClass1.SubClass.CrashDeeper()",
          "void TestAssembly.PrivateClass1.CrashSubclassDeeper()",
          "void TestAssembly.PublicClass1.PrivateCrashSubclassDeeper()"
        );
    }

    [Test]
    public void PrivateCrashSubclassParam()
    {
      DoTest(
          () => new PublicClass1().PrivateCrashSubclassParam(),
          "void TestAssembly.PrivateClass1.SubClass.Crash()",
          "void TestAssembly.PrivateClass1.Crash(TestAssembly.PrivateClass1.SubClass)",
          "void TestAssembly.PrivateClass1.CrashSubclassParam()",
          "void TestAssembly.PublicClass1.PrivateCrashSubclassParam()"
        );
    }
  }
}
