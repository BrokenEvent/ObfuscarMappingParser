using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using ObfuscarMappingParser.Engine;

namespace MappingParser.Tests.Assembly
{
  abstract class BaseAssemblyTests
  {
    private Mapping mapping;
    private System.Reflection.Assembly assembly;
    private Type testType;

    protected void PreLoad(string testFolder, string mappingName)
    {
      ParserConfigs.Instance = new ParserConfigsImpl();
      mapping = new Mapping(TestHelper.TranslatePath($"Assembly\\{testFolder}\\{mappingName}"));

      assembly = System.Reflection.Assembly.LoadFile(TestHelper.TranslatePath($"Assembly\\{testFolder}\\ObfuscarMappingParser.TestAssembly.dll"));

      testType = assembly.GetType("TestAssembly.PublicClass1");
    }

    protected void DoTest(string methodName, params Result[] expectedMethods)
    {
      string stackTrace = null;

      try
      {
        object instance = Activator.CreateInstance(testType);
        MethodInfo method = testType.GetMethod(methodName);
        Assert.NotNull(method, $"Test method not found: {methodName}");

        method.Invoke(instance, new object[0]);
        Assert.Fail("Test method didn't fail.");
      }
      catch (Exception e)
      {
        stackTrace = e.InnerException.StackTrace;
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
          "PublicCrash",
          new Result("void TestAssembly.PublicClass1.PublicCrash()", true)
        );
    }

    [Test]
    public void PrivateCrash()
    {
      DoTest(
          "PrivateCrash",
          new Result("void TestAssembly.PrivateClass1.Crash()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrash()", true)
        );
    }

    [Test]
    public void PrivateCrashDeeper()
    {
      DoTest(
          "PrivateCrashDeeper",
          new Result("void TestAssembly.PrivateClass1.Crash()"),
          new Result("void TestAssembly.PrivateClass1.CrashDeeper()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashDeeper()", true)
        );
    }

    [Test]
    public void PrivateCrashSubclass()
    {
      DoTest(
          "PrivateCrashSubclass",
          new Result("void TestAssembly.PrivateClass1.SubClass.Crash()"),
          new Result("void TestAssembly.PrivateClass1.CrashSubclass()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashSubclass()", true)
        );
    }

    [Test]
    public void PrivateCrashSubclassDeeper()
    {
      DoTest(
          "PrivateCrashSubclassDeeper",
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
          "PrivateCrashSubclassParam",
          new Result("void TestAssembly.PrivateClass1.SubClass.Crash()"),
          new Result("void TestAssembly.PrivateClass1.Crash(TestAssembly.PrivateClass1.SubClass)"),
          new Result("void TestAssembly.PrivateClass1.CrashSubclassParam()"),
          new Result("void TestAssembly.PublicClass1.PrivateCrashSubclassParam()", true)
        );
    }
  }
}
