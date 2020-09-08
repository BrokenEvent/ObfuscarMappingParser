using NUnit.Framework;
using ObfuscarMappingParser.Engine.Items;

namespace MappingParser.Tests
{
  [TestFixture]
  class EntityFullNameTest
  {
    [Test]
    public void SimpleMethodTest()
    {
      Entity name = new Entity("a.aM.A(IDbCommand , String )");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Method, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(2, name.MethodParamsCount);
      Assert.AreEqual("IDbCommand", name.GetMethodParam(0).ToString());
      Assert.AreEqual("String", name.GetMethodParam(1).ToString());
      Assert.AreEqual("void A(IDbCommand, String)", name.NameShort);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameSimple);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameFull);
    }

    [Test]
    public void SimpleMethodWithColonTest()
    {
      Entity name = new Entity("a.aM:A(IDbCommand , String )");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Method, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(2, name.MethodParamsCount);
      Assert.AreEqual("IDbCommand", name.GetMethodParam(0).ToString());
      Assert.AreEqual("String", name.GetMethodParam(1).ToString());
      Assert.AreEqual("void A(IDbCommand, String)", name.NameShort);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameSimple);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameFull);
    }

    [Test]
    public void SimpleMethodWithSpaceTest()
    {
      Entity name = new Entity("a.aM.A ( IDbCommand , String )");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Method, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(2, name.MethodParamsCount);
      Assert.AreEqual("IDbCommand", name.GetMethodParam(0).ToString());
      Assert.AreEqual("String", name.GetMethodParam(1).ToString());
      Assert.AreEqual("void A(IDbCommand, String)", name.NameShort);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameSimple);
      Assert.AreEqual("void a.aM.A(IDbCommand, String)", name.NameFull);
    }

    [Test]
    public void ConstructorTest()
    {
      Entity name = new Entity("a.aM.ctor(IDbCommand , String )");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Constructor, name.EntityType);
      Assert.AreEqual("a.aM.ctor", name.Name.PathName);
      Assert.AreEqual("ctor", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(2, name.MethodParamsCount);
      Assert.AreEqual("IDbCommand", name.GetMethodParam(0).ToString());
      Assert.AreEqual("String", name.GetMethodParam(1).ToString());
      Assert.AreEqual("ctor(IDbCommand, String)", name.NameShort);
      Assert.AreEqual("a.aM.ctor(IDbCommand, String)", name.NameSimple);
      Assert.AreEqual("a.aM.ctor(IDbCommand, String)", name.NameFull);
    }

    [Test]
    public void NsMethodTest()
    {
      Entity name = new Entity("System.String   a.aM.A(IDbCommand a, System.String b)");
      Assert.AreEqual("System.String", name.EntityResultType.ToString());
      Assert.AreEqual(EntityType.Method, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(2, name.MethodParamsCount);
      Assert.AreEqual("IDbCommand", name.GetMethodParam(0).ToString());
      Assert.AreEqual("System.String", name.GetMethodParam(1).ToString());
      Assert.AreEqual("String A(IDbCommand, String)", name.NameShort);
      Assert.AreEqual("String a.aM.A(IDbCommand, String)", name.NameSimple);
      Assert.AreEqual("System.String a.aM.A(IDbCommand, System.String)", name.NameFull);
    }

    [Test]
    public void VoidMethodTest()
    {
      Entity name = new Entity("void   a.aM.A()");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Method, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(0, name.MethodParamsCount);
      Assert.AreEqual("void A()", name.NameShort);
      Assert.AreEqual("void a.aM.A()", name.NameSimple);
      Assert.AreEqual("void a.aM.A()", name.NameFull);
    }

    [Test]
    public void ClassTest()
    {
      Entity name = new Entity("a.aM.A");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Class, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(0, name.MethodParamsCount);
      Assert.AreEqual("A", name.NameShort);
      Assert.AreEqual("a.aM.A", name.NameSimple);
      Assert.AreEqual("a.aM.A", name.NameFull);
    }

    [Test]
    public void FieldTest()
    {
      Entity name = new Entity("System.String a.aM.A");
      Assert.AreEqual("System.String", name.EntityResultType.ToString());
      Assert.AreEqual(EntityType.Field, name.EntityType);
      Assert.AreEqual("a.aM.A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.AreEqual("a.aM", name.Name.Namespace);
      Assert.AreEqual(0, name.MethodParamsCount);
      Assert.AreEqual("String A", name.NameShort);
      Assert.AreEqual("String a.aM.A", name.NameSimple);
      Assert.AreEqual("System.String a.aM.A", name.NameFull);
    }

    [Test]
    public void SimpleNameTest()
    {
      Entity name = new Entity("A");
      Assert.IsNull(name.EntityResultType);
      Assert.AreEqual(EntityType.Class, name.EntityType);
      Assert.AreEqual("A", name.Name.PathName);
      Assert.AreEqual("A", name.Name.Name);
      Assert.IsNull(name.Name.Namespace);
      Assert.AreEqual(0, name.MethodParamsCount);
      Assert.AreEqual("A", name.NameShort);
      Assert.AreEqual("A", name.NameSimple);
      Assert.AreEqual("A", name.NameFull);
    }
  }
}
