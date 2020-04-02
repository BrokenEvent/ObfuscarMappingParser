using NUnit.Framework;
using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;

namespace MappingParser.Tests
{
  [TestFixture]
  class NamingTest
  {
    private static TType GetItemByNewName<TType>(RenamedClass c, string newName)
      where TType: RenamedBase
    {
      foreach (RenamedBase item in c.Items)
      {
        if (item.Name.NameNew.Name == newName)
          return (TType)item;
      }

      Assert.Fail($"{newName} not found");
      return null;
    }

    [SetUp]
    public void SetUp()
    {
      ParserConfigs.Instance = new ParserConfigsImpl();
    }

    [Test]
    public void ClassTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);

      // [ModuleOld]NsOld.ClassOld -> [ModuleNew]NsNew.ClassNew
      RenamedClass renamedClass = mapping.Classes[0];
      Assert.AreEqual(19, renamedClass.Items.Count);
      Assert.AreEqual("ModuleOld", renamedClass.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedClass.ModuleNew);
      Assert.AreEqual("NsOld.ClassOld", renamedClass.NameOld);
      Assert.AreEqual("NsNew.ClassNew", renamedClass.NameNew);
      Assert.AreEqual("NsOld.ClassOld", renamedClass.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew", renamedClass.NameNewPlain);
      Assert.AreEqual("NsOld.ClassOld", renamedClass.NameOldSimple);
      Assert.AreEqual("NsNew.ClassNew", renamedClass.NameNewSimple);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld", renamedClass.NameOldFull);
      Assert.AreEqual("[ModuleNew]NsNew.ClassNew", renamedClass.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld → NsNew.ClassNew", renamedClass.TransformName);
      Assert.AreEqual("NsOld.ClassOld → NsNew.ClassNew", renamedClass.TransformSimple);
      Assert.AreEqual("NsOld → NsNew", renamedClass.TransformNamespace);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld → [ModuleNew]NsNew.ClassNew", renamedClass.TransformNameFull);
      Assert.AreEqual("NsOld.ClassOld → NsNew.ClassNew", renamedClass.ToString());
    }

    [Test]
    public void FieldTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld NsOld.ClassOld::ClassFieldOld -> ClassFieldNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "ClassFieldNew");
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual(renamedClass.Name.NameOld, renamedItem.ResultType.NameOld);
      Assert.AreEqual(renamedClass.Name.NameNew, renamedItem.ResultType.NameNew);
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("ClassOld ClassFieldOld", renamedItem.NameOld);
      Assert.AreEqual("ClassNew ClassFieldNew", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.ClassFieldOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.ClassFieldNew", renamedItem.NameNewPlain);
      Assert.AreEqual("NsOld.ClassOld NsOld.ClassOld.ClassFieldOld", renamedItem.NameOldFull);
      Assert.AreEqual("NsNew.ClassNew NsNew.ClassNew.ClassFieldNew", renamedItem.NameNewFull);
      Assert.AreEqual("ClassOld NsOld.ClassOld.ClassFieldOld", renamedItem.NameOldSimple);
      Assert.AreEqual("ClassNew NsNew.ClassNew.ClassFieldNew", renamedItem.NameNewSimple);
      Assert.AreEqual("ClassOld ClassFieldOld → ClassNew ClassFieldNew", renamedItem.TransformName);
      Assert.AreEqual("NsOld.ClassOld NsOld.ClassOld.ClassFieldOld → NsNew.ClassNew NsNew.ClassNew.ClassFieldNew", renamedItem.TransformNameFull);
      Assert.AreEqual("ClassOld NsOld.ClassOld.ClassFieldOld → ClassNew NsNew.ClassNew.ClassFieldNew", renamedItem.TransformSimple);
      Assert.AreEqual("ClassOld NsOld.ClassOld.ClassFieldOld → ClassNew NsNew.ClassNew.ClassFieldNew", renamedItem.ToString());
    }

    [Test]
    public void GenericFieldTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.Collections.Generic.Dictionary`2<System.String,System.String> NsOld.ClassOld::GenericFieldOld -> GenericFieldNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "GenericFieldNew");
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.String>", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.String>", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("Dictionary<String, String> GenericFieldOld", renamedItem.NameOld);
      Assert.AreEqual("Dictionary<String, String> GenericFieldNew", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewPlain);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.String> NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldFull);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.String> NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewFull);
      Assert.AreEqual("Dictionary<String, String> NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldSimple);
      Assert.AreEqual("Dictionary<String, String> NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewSimple);
      Assert.AreEqual("Dictionary<String, String> GenericFieldOld → Dictionary<String, String> GenericFieldNew", renamedItem.TransformName);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.String> NsOld.ClassOld.GenericFieldOld → System.Collections.Generic.Dictionary<System.String, System.String> NsNew.ClassNew.GenericFieldNew", renamedItem.TransformNameFull);
      Assert.AreEqual("Dictionary<String, String> NsOld.ClassOld.GenericFieldOld → Dictionary<String, String> NsNew.ClassNew.GenericFieldNew", renamedItem.TransformSimple);
      Assert.AreEqual("Dictionary<String, String> NsOld.ClassOld.GenericFieldOld → Dictionary<String, String> NsNew.ClassNew.GenericFieldNew", renamedItem.ToString());
    }

    [Test]
    public void GenericFieldSimplfyTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.Collections.Generic.Dictionary`2<System.String,System.String> NsOld.ClassOld::GenericFieldOld -> GenericFieldNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "GenericFieldNew");
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.Collections.Generic.Dictionary<string, string>", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.Collections.Generic.Dictionary<string, string>", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("Dictionary<string, string> GenericFieldOld", renamedItem.NameOld);
      Assert.AreEqual("Dictionary<string, string> GenericFieldNew", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewPlain);
      Assert.AreEqual("System.Collections.Generic.Dictionary<string, string> NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldFull);
      Assert.AreEqual("System.Collections.Generic.Dictionary<string, string> NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewFull);
      Assert.AreEqual("Dictionary<string, string> NsOld.ClassOld.GenericFieldOld", renamedItem.NameOldSimple);
      Assert.AreEqual("Dictionary<string, string> NsNew.ClassNew.GenericFieldNew", renamedItem.NameNewSimple);
      Assert.AreEqual("Dictionary<string, string> GenericFieldOld → Dictionary<string, string> GenericFieldNew", renamedItem.TransformName);
      Assert.AreEqual("System.Collections.Generic.Dictionary<string, string> NsOld.ClassOld.GenericFieldOld → System.Collections.Generic.Dictionary<string, string> NsNew.ClassNew.GenericFieldNew", renamedItem.TransformNameFull);
      Assert.AreEqual("Dictionary<string, string> NsOld.ClassOld.GenericFieldOld → Dictionary<string, string> NsNew.ClassNew.GenericFieldNew", renamedItem.TransformSimple);
      Assert.AreEqual("Dictionary<string, string> NsOld.ClassOld.GenericFieldOld → Dictionary<string, string> NsNew.ClassNew.GenericFieldNew", renamedItem.ToString());
    }

    [Test]
    public void PropertyTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.String NsOld.ClassOld::StringFieldOld -> StringFieldNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "StringFieldNew");
      Assert.AreEqual(EntityType.Property, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.String", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("String StringFieldOld", renamedItem.NameOld);
      Assert.AreEqual("String StringFieldNew", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.StringFieldOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.StringFieldNew", renamedItem.NameNewPlain);
      Assert.AreEqual("System.String NsOld.ClassOld.StringFieldOld", renamedItem.NameOldFull);
      Assert.AreEqual("System.String NsNew.ClassNew.StringFieldNew", renamedItem.NameNewFull);
      Assert.AreEqual("String NsOld.ClassOld.StringFieldOld", renamedItem.NameOldSimple);
      Assert.AreEqual("String NsNew.ClassNew.StringFieldNew", renamedItem.NameNewSimple);
      Assert.AreEqual("String StringFieldOld → String StringFieldNew", renamedItem.TransformName);
      Assert.AreEqual("System.String NsOld.ClassOld.StringFieldOld → System.String NsNew.ClassNew.StringFieldNew", renamedItem.TransformNameFull);
      Assert.AreEqual("String NsOld.ClassOld.StringFieldOld → String NsNew.ClassNew.StringFieldNew", renamedItem.TransformSimple);
      Assert.AreEqual("String NsOld.ClassOld.StringFieldOld → String NsNew.ClassNew.StringFieldNew", renamedItem.ToString());
    }

    [Test]
    public void PropertySimplifyTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.String NsOld.ClassOld::StringFieldOld -> StringFieldNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "StringFieldNew");
      Assert.AreEqual(EntityType.Property, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.String", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("string StringFieldOld", renamedItem.NameOld);
      Assert.AreEqual("string StringFieldNew", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.StringFieldOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.StringFieldNew", renamedItem.NameNewPlain);
      Assert.AreEqual("string NsOld.ClassOld.StringFieldOld", renamedItem.NameOldFull);
      Assert.AreEqual("string NsNew.ClassNew.StringFieldNew", renamedItem.NameNewFull);
      Assert.AreEqual("string NsOld.ClassOld.StringFieldOld", renamedItem.NameOldSimple);
      Assert.AreEqual("string NsNew.ClassNew.StringFieldNew", renamedItem.NameNewSimple);
      Assert.AreEqual("string StringFieldOld → string StringFieldNew", renamedItem.TransformName);
      Assert.AreEqual("string NsOld.ClassOld.StringFieldOld → string NsNew.ClassNew.StringFieldNew", renamedItem.TransformNameFull);
      Assert.AreEqual("string NsOld.ClassOld.StringFieldOld → string NsNew.ClassNew.StringFieldNew", renamedItem.TransformSimple);
      Assert.AreEqual("string NsOld.ClassOld.StringFieldOld → string NsNew.ClassNew.StringFieldNew", renamedItem.ToString());
    }

    [Test]
    public void StringResultMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld NsOld.ClassOld::StringMethodOld() -> StringMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "StringMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.String", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(0, renamedItem.MethodParams.Count);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("String StringMethodOld()", renamedItem.NameOld);
      Assert.AreEqual("String StringMethodNew()", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.StringMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.StringMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("System.String NsOld.ClassOld.StringMethodOld()", renamedItem.NameOldFull);
      Assert.AreEqual("System.String NsNew.ClassNew.StringMethodNew()", renamedItem.NameNewFull);
      Assert.AreEqual("String NsOld.ClassOld.StringMethodOld()", renamedItem.NameOldSimple);
      Assert.AreEqual("String NsNew.ClassNew.StringMethodNew()", renamedItem.NameNewSimple);
      Assert.AreEqual("String StringMethodOld() → String StringMethodNew()", renamedItem.TransformName);
      Assert.AreEqual("System.String NsOld.ClassOld.StringMethodOld() → System.String NsNew.ClassNew.StringMethodNew()", renamedItem.TransformNameFull);
      Assert.AreEqual("String NsOld.ClassOld.StringMethodOld() → String NsNew.ClassNew.StringMethodNew()", renamedItem.TransformSimple);
      Assert.AreEqual("String NsOld.ClassOld.StringMethodOld() → String NsNew.ClassNew.StringMethodNew()", renamedItem.ToString());      
    }

    [Test]
    public void StringParamMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::MethodWithParamOld(System.String) -> MethodWithParamNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "MethodWithParamNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.String", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void MethodWithParamOld(String)", renamedItem.NameOld);
      Assert.AreEqual("void MethodWithParamNew(String)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.MethodWithParamOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.MethodWithParamNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.MethodWithParamOld(System.String)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.MethodWithParamNew(System.String)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.MethodWithParamOld(String)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.MethodWithParamNew(String)", renamedItem.NameNewSimple);
      Assert.AreEqual("void MethodWithParamOld(String) → void MethodWithParamNew(String)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.MethodWithParamOld(System.String) → void NsNew.ClassNew.MethodWithParamNew(System.String)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.MethodWithParamOld(String) → void NsNew.ClassNew.MethodWithParamNew(String)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.MethodWithParamOld(String) → void NsNew.ClassNew.MethodWithParamNew(String)", renamedItem.ToString());
    }

    [Test]
    public void GenericMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::GenericMethodOld(System.Collections.Generic.Dictionary`2<System.String,NsOld.ClassOld>) -> GenericMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "GenericMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, NsOld.ClassOld>", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, NsNew.ClassNew>", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void GenericMethodOld(Dictionary<String, ClassOld>)", renamedItem.NameOld);
      Assert.AreEqual("void GenericMethodNew(Dictionary<String, ClassNew>)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld(System.Collections.Generic.Dictionary<System.String, NsOld.ClassOld>)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew(System.Collections.Generic.Dictionary<System.String, NsNew.ClassNew>)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld(Dictionary<String, ClassOld>)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew(Dictionary<String, ClassNew>)", renamedItem.NameNewSimple);
      Assert.AreEqual("void GenericMethodOld(Dictionary<String, ClassOld>) → void GenericMethodNew(Dictionary<String, ClassNew>)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld(System.Collections.Generic.Dictionary<System.String, NsOld.ClassOld>) → void NsNew.ClassNew.GenericMethodNew(System.Collections.Generic.Dictionary<System.String, NsNew.ClassNew>)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld(Dictionary<String, ClassOld>) → void NsNew.ClassNew.GenericMethodNew(Dictionary<String, ClassNew>)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld(Dictionary<String, ClassOld>) → void NsNew.ClassNew.GenericMethodNew(Dictionary<String, ClassNew>)", renamedItem.ToString());
    }

    [Test]
    public void RefArgTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::RefMethodOld([mscorlib]System.Int32&) -> RefMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "RefMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32&", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32&", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32&", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void RefMethodOld(Int32&)", renamedItem.NameOld);
      Assert.AreEqual("void RefMethodNew(Int32&)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.RefMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.RefMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(System.Int32&)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.RefMethodNew(System.Int32&)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(Int32&)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.RefMethodNew(Int32&)", renamedItem.NameNewSimple);
      Assert.AreEqual("void RefMethodOld(Int32&) → void RefMethodNew(Int32&)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(System.Int32&) → void NsNew.ClassNew.RefMethodNew(System.Int32&)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(Int32&) → void NsNew.ClassNew.RefMethodNew(Int32&)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(Int32&) → void NsNew.ClassNew.RefMethodNew(Int32&)", renamedItem.ToString());
    }

    [Test]
    public void RefArgSimplifyTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = true;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::RefMethodOld([mscorlib]System.Int32&) -> RefMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "RefMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("ref System.Int32", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("ref System.Int32", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("ref System.Int32", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void RefMethodOld(ref Int32)", renamedItem.NameOld);
      Assert.AreEqual("void RefMethodNew(ref Int32)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.RefMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.RefMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(ref System.Int32)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.RefMethodNew(ref System.Int32)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(ref Int32)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.RefMethodNew(ref Int32)", renamedItem.NameNewSimple);
      Assert.AreEqual("void RefMethodOld(ref Int32) → void RefMethodNew(ref Int32)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(ref System.Int32) → void NsNew.ClassNew.RefMethodNew(ref System.Int32)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(ref Int32) → void NsNew.ClassNew.RefMethodNew(ref Int32)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.RefMethodOld(ref Int32) → void NsNew.ClassNew.RefMethodNew(ref Int32)", renamedItem.ToString());
    }

    [Test]
    public void PtrArgTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::PtrMethodOld([mscorlib]System.Int32*) -> PtrMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "PtrMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32*", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32*", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32*", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void PtrMethodOld(Int32*)", renamedItem.NameOld);
      Assert.AreEqual("void PtrMethodNew(Int32*)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.PtrMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.PtrMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.PtrMethodOld(System.Int32*)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.PtrMethodNew(System.Int32*)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.PtrMethodOld(Int32*)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.PtrMethodNew(Int32*)", renamedItem.NameNewSimple);
      Assert.AreEqual("void PtrMethodOld(Int32*) → void PtrMethodNew(Int32*)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.PtrMethodOld(System.Int32*) → void NsNew.ClassNew.PtrMethodNew(System.Int32*)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.PtrMethodOld(Int32*) → void NsNew.ClassNew.PtrMethodNew(Int32*)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.PtrMethodOld(Int32*) → void NsNew.ClassNew.PtrMethodNew(Int32*)", renamedItem.ToString());
    }

    [Test]
    public void RefPtrArgTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::RefPtrMethodOld([mscorlib]System.Int32*&) -> RefPtrMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "RefPtrMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32*&", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32*&", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32*&", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void RefPtrMethodOld(Int32*&)", renamedItem.NameOld);
      Assert.AreEqual("void RefPtrMethodNew(Int32*&)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.RefPtrMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.RefPtrMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.RefPtrMethodOld(System.Int32*&)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.RefPtrMethodNew(System.Int32*&)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.RefPtrMethodOld(Int32*&)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.RefPtrMethodNew(Int32*&)", renamedItem.NameNewSimple);
      Assert.AreEqual("void RefPtrMethodOld(Int32*&) → void RefPtrMethodNew(Int32*&)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.RefPtrMethodOld(System.Int32*&) → void NsNew.ClassNew.RefPtrMethodNew(System.Int32*&)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.RefPtrMethodOld(Int32*&) → void NsNew.ClassNew.RefPtrMethodNew(Int32*&)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.RefPtrMethodOld(Int32*&) → void NsNew.ClassNew.RefPtrMethodNew(Int32*&)", renamedItem.ToString());
    }

    [Test]
    public void OneDimensionalArrayTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::ArrayOldSet1([mscorlib]System.Int32[]) -> ArrayNewSet1
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "ArrayNewSet1");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32[]", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32[]", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32[]", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void ArrayOldSet1(Int32[])", renamedItem.NameOld);
      Assert.AreEqual("void ArrayNewSet1(Int32[])", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.ArrayOldSet1", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.ArrayNewSet1", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet1(System.Int32[])", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.ArrayNewSet1(System.Int32[])", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet1(Int32[])", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.ArrayNewSet1(Int32[])", renamedItem.NameNewSimple);
      Assert.AreEqual("void ArrayOldSet1(Int32[]) → void ArrayNewSet1(Int32[])", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet1(System.Int32[]) → void NsNew.ClassNew.ArrayNewSet1(System.Int32[])", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet1(Int32[]) → void NsNew.ClassNew.ArrayNewSet1(Int32[])", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet1(Int32[]) → void NsNew.ClassNew.ArrayNewSet1(Int32[])", renamedItem.ToString());
    }

    [Test]
    public void TwoDimensionalArrayTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::ArrayOldSet2([mscorlib]System.Int32[][]) -> ArrayNewSet2
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "ArrayNewSet2");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32[][]", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32[][]", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32[][]", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void ArrayOldSet2(Int32[][])", renamedItem.NameOld);
      Assert.AreEqual("void ArrayNewSet2(Int32[][])", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.ArrayOldSet2", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.ArrayNewSet2", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet2(System.Int32[][])", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.ArrayNewSet2(System.Int32[][])", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet2(Int32[][])", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.ArrayNewSet2(Int32[][])", renamedItem.NameNewSimple);
      Assert.AreEqual("void ArrayOldSet2(Int32[][]) → void ArrayNewSet2(Int32[][])", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet2(System.Int32[][]) → void NsNew.ClassNew.ArrayNewSet2(System.Int32[][])", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet2(Int32[][]) → void NsNew.ClassNew.ArrayNewSet2(Int32[][])", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.ArrayOldSet2(Int32[][]) → void NsNew.ClassNew.ArrayNewSet2(Int32[][])", renamedItem.ToString());
    }

    // no tests for square 2d array, as the obfuscar produces the same output as for 1d: System.Int32[]

    [Test]
    public void PtrArrayTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::PtrArrayOldSet([mscorlib]System.Int32*[]) -> PtrArrayNewSet
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "PtrArrayNewSet");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Int32*[]", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Int32*[]", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Int32*[]", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void PtrArrayOldSet(Int32*[])", renamedItem.NameOld);
      Assert.AreEqual("void PtrArrayNewSet(Int32*[])", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.PtrArrayOldSet", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.PtrArrayNewSet", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.PtrArrayOldSet(System.Int32*[])", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.PtrArrayNewSet(System.Int32*[])", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.PtrArrayOldSet(Int32*[])", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.PtrArrayNewSet(Int32*[])", renamedItem.NameNewSimple);
      Assert.AreEqual("void PtrArrayOldSet(Int32*[]) → void PtrArrayNewSet(Int32*[])", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.PtrArrayOldSet(System.Int32*[]) → void NsNew.ClassNew.PtrArrayNewSet(System.Int32*[])", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.PtrArrayOldSet(Int32*[]) → void NsNew.ClassNew.PtrArrayNewSet(Int32*[])", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.PtrArrayOldSet(Int32*[]) → void NsNew.ClassNew.PtrArrayNewSet(Int32*[])", renamedItem.ToString());
    }

    [Test]
    public void EmbeddedGenericMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::GenericSetOld[1]([mscorlib]System.Collections.Generic.Dictionary`2<[mscorlib]System.String,[mscorlib]System.Collections.Generic.List`1<[mscorlib]System.String>>)
      // -> GenericSetNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "GenericSetNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void GenericSetOld(Dictionary<String, List<String>>)", renamedItem.NameOld);
      Assert.AreEqual("void GenericSetNew(Dictionary<String, List<String>>)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericSetOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericSetNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.GenericSetOld(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.GenericSetNew(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericSetOld(Dictionary<String, List<String>>)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.GenericSetNew(Dictionary<String, List<String>>)", renamedItem.NameNewSimple);
      Assert.AreEqual("void GenericSetOld(Dictionary<String, List<String>>) → void GenericSetNew(Dictionary<String, List<String>>)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.GenericSetOld(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>) → void NsNew.ClassNew.GenericSetNew(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.String>>)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericSetOld(Dictionary<String, List<String>>) → void NsNew.ClassNew.GenericSetNew(Dictionary<String, List<String>>)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.GenericSetOld(Dictionary<String, List<String>>) → void NsNew.ClassNew.GenericSetNew(Dictionary<String, List<String>>)", renamedItem.ToString());
    }

    [Test]
    public void NullableArgMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyNullable = false;

      // [ModuleOld]NsOld.ClassOld::NullableRefMethodOld([mscorlib]System.Nullable`1<[mscorlib]System.Int32>&) -> NullableRefMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "NullableRefMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Nullable<System.Int32>&", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Nullable<System.Int32>&", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Nullable<System.Int32>&", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void NullableRefMethodOld(Nullable<Int32>&)", renamedItem.NameOld);
      Assert.AreEqual("void NullableRefMethodNew(Nullable<Int32>&)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.NullableRefMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.NullableRefMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.NullableRefMethodOld(System.Nullable<System.Int32>&)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.NullableRefMethodNew(System.Nullable<System.Int32>&)", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.NullableRefMethodOld(Nullable<Int32>&)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.NullableRefMethodNew(Nullable<Int32>&)", renamedItem.NameNewSimple);
      Assert.AreEqual("void NullableRefMethodOld(Nullable<Int32>&) → void NullableRefMethodNew(Nullable<Int32>&)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.NullableRefMethodOld(System.Nullable<System.Int32>&) → void NsNew.ClassNew.NullableRefMethodNew(System.Nullable<System.Int32>&)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.NullableRefMethodOld(Nullable<Int32>&) → void NsNew.ClassNew.NullableRefMethodNew(Nullable<Int32>&)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.NullableRefMethodOld(Nullable<Int32>&) → void NsNew.ClassNew.NullableRefMethodNew(Nullable<Int32>&)", renamedItem.ToString());
    }

    [Test]
    public void TwoParamMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.String NsOld.ClassOld::MethodWithParam2Old([ModuleOld]NsOld.ClassOld,System.String) -> MethodWithParam2New
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "MethodWithParam2New");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.String", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(2, renamedItem.MethodParams.Count);
      Assert.AreEqual("NsOld.ClassOld", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("NsNew.ClassNew", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.String", renamedItem.MethodParams[1].NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.MethodParams[1].NameNew.ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("String MethodWithParam2Old(ClassOld, String)", renamedItem.NameOld);
      Assert.AreEqual("String MethodWithParam2New(ClassNew, String)", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.MethodWithParam2Old", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.MethodWithParam2New", renamedItem.NameNewPlain);
      Assert.AreEqual("System.String NsOld.ClassOld.MethodWithParam2Old(NsOld.ClassOld, System.String)", renamedItem.NameOldFull);
      Assert.AreEqual("System.String NsNew.ClassNew.MethodWithParam2New(NsNew.ClassNew, System.String)", renamedItem.NameNewFull);
      Assert.AreEqual("String NsOld.ClassOld.MethodWithParam2Old(ClassOld, String)", renamedItem.NameOldSimple);
      Assert.AreEqual("String NsNew.ClassNew.MethodWithParam2New(ClassNew, String)", renamedItem.NameNewSimple);
      Assert.AreEqual("String MethodWithParam2Old(ClassOld, String) → String MethodWithParam2New(ClassNew, String)", renamedItem.TransformName);
      Assert.AreEqual("System.String NsOld.ClassOld.MethodWithParam2Old(NsOld.ClassOld, System.String) → System.String NsNew.ClassNew.MethodWithParam2New(NsNew.ClassNew, System.String)", renamedItem.TransformNameFull);
      Assert.AreEqual("String NsOld.ClassOld.MethodWithParam2Old(ClassOld, String) → String NsNew.ClassNew.MethodWithParam2New(ClassNew, String)", renamedItem.TransformSimple);
      Assert.AreEqual("String NsOld.ClassOld.MethodWithParam2Old(ClassOld, String) → String NsNew.ClassNew.MethodWithParam2New(ClassNew, String)", renamedItem.ToString());
    }

    [Test]
    public void SomethingWickedTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::<Initialize>m__D[0]() -> I
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(renamedClass, "I");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(0, renamedItem.MethodParams.Count);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void <Initialize>m__D()", renamedItem.NameOld);
      Assert.AreEqual("void I()", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.<Initialize>m__D", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.I", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.<Initialize>m__D()", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.I()", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.<Initialize>m__D()", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.I()", renamedItem.NameNewSimple);
      Assert.AreEqual("void <Initialize>m__D() → void I()", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.<Initialize>m__D() → void NsNew.ClassNew.I()", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.<Initialize>m__D() → void NsNew.ClassNew.I()", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.<Initialize>m__D() → void NsNew.ClassNew.I()", renamedItem.ToString());     
    }

    [Test]
    public void SubclassTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld/SubclassOld -> [ModuleNew]NsNew.ClassNew/SubclassNew
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SubclassNew");
      Assert.AreEqual(3, subclass.Items.Count);
      Assert.AreEqual("ModuleOld", subclass.ModuleOld);
      Assert.AreEqual("ModuleNew", subclass.ModuleNew);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld", subclass.NameOld);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew", subclass.NameNew);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld", subclass.NameOldSimple);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew", subclass.NameNewSimple);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld", subclass.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew", subclass.NameNewPlain);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SubclassOld", subclass.NameOldFull);
      Assert.AreEqual("[ModuleNew]NsNew.ClassNew.SubclassNew", subclass.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld → NsNew.ClassNew.SubclassNew", subclass.TransformName);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld → NsNew.ClassNew.SubclassNew", subclass.TransformSimple);
      Assert.AreEqual("NsOld → NsNew", subclass.TransformNamespace);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SubclassOld → [ModuleNew]NsNew.ClassNew.SubclassNew", subclass.TransformNameFull);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld → NsNew.ClassNew.SubclassNew", subclass.ToString());
    }

    [Test]
    public void SubclassMethodTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SubclassNew");

      // [ModuleOld]System.String NsOld.ClassOld/SubclassOld::StringMethodOld() -> StringMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(subclass, "StringMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.String", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.String", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(0, renamedItem.MethodParams.Count);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(subclass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("String StringMethodOld()", renamedItem.NameOld);
      Assert.AreEqual("String StringMethodNew()", renamedItem.NameNew);
      Assert.AreEqual("System.String NsOld.ClassOld.SubclassOld.StringMethodOld()", renamedItem.NameOldFull);
      Assert.AreEqual("System.String NsNew.ClassNew.SubclassNew.StringMethodNew()", renamedItem.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld.StringMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew.StringMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("String NsOld.ClassOld.SubclassOld.StringMethodOld()", renamedItem.NameOldSimple);
      Assert.AreEqual("String NsNew.ClassNew.SubclassNew.StringMethodNew()", renamedItem.NameNewSimple);
      Assert.AreEqual("String StringMethodOld() → String StringMethodNew()", renamedItem.TransformName);
      Assert.AreEqual("System.String NsOld.ClassOld.SubclassOld.StringMethodOld() → System.String NsNew.ClassNew.SubclassNew.StringMethodNew()", renamedItem.TransformNameFull);
      Assert.AreEqual("String NsOld.ClassOld.SubclassOld.StringMethodOld() → String NsNew.ClassNew.SubclassNew.StringMethodNew()", renamedItem.TransformSimple);
      Assert.AreEqual("String NsOld.ClassOld.SubclassOld.StringMethodOld() → String NsNew.ClassNew.SubclassNew.StringMethodNew()", renamedItem.ToString());
    }

    [Test]
    public void SubclassMethodWithParamTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SubclassNew");

      // [ModuleOld]NsOld.ClassOld/SubclassOld::SubclassMethodOld(NsOld.ClassOld/SubclassOld) -> SubclassMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(subclass, "SubclassMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("NsNew.ClassNew.SubclassNew", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual(new EntityName(subclass.NameOld), renamedItem.MethodParams[0].NameOld);
      Assert.AreEqual(new EntityName(subclass.NameNew), renamedItem.MethodParams[0].NameNew);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(subclass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void SubclassMethodOld(SubclassOld)", renamedItem.NameOld);
      Assert.AreEqual("void SubclassMethodNew(SubclassNew)", renamedItem.NameNew);
      Assert.AreEqual("void NsOld.ClassOld.SubclassOld.SubclassMethodOld(NsOld.ClassOld.SubclassOld)", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.SubclassNew.SubclassMethodNew(NsNew.ClassNew.SubclassNew)", renamedItem.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld.SubclassMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew.SubclassMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.SubclassOld.SubclassMethodOld(SubclassOld)", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.SubclassNew.SubclassMethodNew(SubclassNew)", renamedItem.NameNewSimple);
      Assert.AreEqual("void SubclassMethodOld(SubclassOld) → void SubclassMethodNew(SubclassNew)", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.SubclassOld.SubclassMethodOld(NsOld.ClassOld.SubclassOld) → void NsNew.ClassNew.SubclassNew.SubclassMethodNew(NsNew.ClassNew.SubclassNew)", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.SubclassOld.SubclassMethodOld(SubclassOld) → void NsNew.ClassNew.SubclassNew.SubclassMethodNew(SubclassNew)", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.SubclassOld.SubclassMethodOld(SubclassOld) → void NsNew.ClassNew.SubclassNew.SubclassMethodNew(SubclassNew)", renamedItem.ToString());
    }

    [Test]
    public void SubclassMethodWithResultTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SubclassNew");

      // [ModuleOld]NsOld.ClassOld/SubclassOld NsOld.ClassOld/SubclassOld::SubclassResultMethodOld() -> SubclassResultMethodNew
      RenamedItem renamedItem = GetItemByNewName<RenamedItem>(subclass, "SubclassResultMethodNew");
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("NsNew.ClassNew.SubclassNew", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(0, renamedItem.MethodParams.Count);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(subclass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("SubclassOld SubclassResultMethodOld()", renamedItem.NameOld);
      Assert.AreEqual("SubclassNew SubclassResultMethodNew()", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld NsOld.ClassOld.SubclassOld.SubclassResultMethodOld()", renamedItem.NameOldFull);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew NsNew.ClassNew.SubclassNew.SubclassResultMethodNew()", renamedItem.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld.SubclassResultMethodOld", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SubclassNew.SubclassResultMethodNew", renamedItem.NameNewPlain);
      Assert.AreEqual("SubclassOld NsOld.ClassOld.SubclassOld.SubclassResultMethodOld()", renamedItem.NameOldSimple);
      Assert.AreEqual("SubclassNew NsNew.ClassNew.SubclassNew.SubclassResultMethodNew()", renamedItem.NameNewSimple);
      Assert.AreEqual("SubclassOld SubclassResultMethodOld() → SubclassNew SubclassResultMethodNew()", renamedItem.TransformName);
      Assert.AreEqual("NsOld.ClassOld.SubclassOld NsOld.ClassOld.SubclassOld.SubclassResultMethodOld() → NsNew.ClassNew.SubclassNew NsNew.ClassNew.SubclassNew.SubclassResultMethodNew()", renamedItem.TransformNameFull);
      Assert.AreEqual("SubclassOld NsOld.ClassOld.SubclassOld.SubclassResultMethodOld() → SubclassNew NsNew.ClassNew.SubclassNew.SubclassResultMethodNew()", renamedItem.TransformSimple);
      Assert.AreEqual("SubclassOld NsOld.ClassOld.SubclassOld.SubclassResultMethodOld() → SubclassNew NsNew.ClassNew.SubclassNew.SubclassResultMethodNew()", renamedItem.ToString());
    }

    [Test]
    public void SkippedByOldNameSubclassTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // skipped [ModuleOld]NsOld.ClassOld/SkippedSubclass1
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SkippedSubclass1");
      Assert.AreEqual(0, subclass.Items.Count);
      Assert.IsNotNull(subclass.SkipReason);
      Assert.AreEqual("ModuleOld", subclass.ModuleOld);
      Assert.AreEqual("ModuleNew", subclass.ModuleNew);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1", subclass.NameOld);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass1", subclass.NameNew);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1", subclass.NameOldSimple);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass1", subclass.NameNewSimple);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SkippedSubclass1", subclass.NameOldFull);
      Assert.AreEqual("[ModuleNew]NsNew.ClassNew.SkippedSubclass1", subclass.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1", subclass.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass1", subclass.NameNewPlain);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1 → NsNew.ClassNew.SkippedSubclass1", subclass.TransformName);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1 → NsNew.ClassNew.SkippedSubclass1", subclass.TransformSimple);
      Assert.AreEqual("NsOld → NsNew", subclass.TransformNamespace);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SkippedSubclass1 → [ModuleNew]NsNew.ClassNew.SkippedSubclass1", subclass.TransformNameFull);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass1 → NsNew.ClassNew.SkippedSubclass1", subclass.ToString());
    }

    [Test]
    public void SkippedByNewNameSubclassTest()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // skipped [ModuleOld]NsOld.ClassOld/SkippedSubclass2
      RenamedClass subclass = GetItemByNewName<RenamedClass>(renamedClass, "SkippedSubclass2");
      Assert.AreEqual(0, subclass.Items.Count);
      Assert.IsNotNull(subclass.SkipReason);
      Assert.AreEqual("ModuleOld", subclass.ModuleOld);
      Assert.AreEqual("ModuleNew", subclass.ModuleNew);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2", subclass.NameOld);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass2", subclass.NameNew);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2", subclass.NameOldSimple);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass2", subclass.NameNewSimple);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SkippedSubclass2", subclass.NameOldFull);
      Assert.AreEqual("[ModuleNew]NsNew.ClassNew.SkippedSubclass2", subclass.NameNewFull);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2", subclass.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.SkippedSubclass2", subclass.NameNewPlain);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2 → NsNew.ClassNew.SkippedSubclass2", subclass.TransformName);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2 → NsNew.ClassNew.SkippedSubclass2", subclass.TransformSimple);
      Assert.AreEqual("NsOld → NsNew", subclass.TransformNamespace);
      Assert.AreEqual("[ModuleOld]NsOld.ClassOld.SkippedSubclass2 → [ModuleNew]NsNew.ClassNew.SkippedSubclass2", subclass.TransformNameFull);
      Assert.AreEqual("NsOld.ClassOld.SkippedSubclass2 → NsNew.ClassNew.SkippedSubclass2", subclass.ToString());
    }

    [Test]
    public void StatisticsTest()
    {
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\NamingTestMapping.xml"));
      Assert.AreEqual(16, mapping.TotalMethodsCount);
      Assert.AreEqual(4, mapping.TotalClassesCount);
      Assert.AreEqual(3, mapping.TotalSubclassesCount);
      Assert.AreEqual(1, mapping.NamespacesCount);
      Assert.AreEqual(1, mapping.ObfuscatedNamespacesCount);
      Assert.AreEqual(1, mapping.ModulesCount);
      Assert.AreEqual(2, mapping.SkippedEntities);
    }

    [Test]
    public void RealNamingTest1()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassSecondOld<System.String,System.Runtime.CompilerServices.CallSite`1<System.Func`3<System.Runtime.CompilerServices.CallSite,System.Object,System.Object>>> NsOld.ClassOld::_callSiteGetters
      // -> GenericFieldNew1
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[0];
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>>", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>>", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("ClassSecondOld<String, CallSite<Func<CallSite, Object, Object>>> _callSiteGetters", renamedItem.NameOld);
      Assert.AreEqual("ClassSecondOld<String, CallSite<Func<CallSite, Object, Object>>> GenericFieldNew1", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld._callSiteGetters", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew1", renamedItem.NameNewPlain);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>> NsOld.ClassOld._callSiteGetters", renamedItem.NameOldFull);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>> NsNew.ClassNew.GenericFieldNew1", renamedItem.NameNewFull);
    }

    [Test]
    public void RealNamingTest2()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassSecondOld<System.String,System.Runtime.CompilerServices.CallSite`1<System.Func`4<System.Runtime.CompilerServices.CallSite,System.Object,System.Object,System.Object>>> NsOld.ClassOld::_callSiteSetters
      // -> GenericFieldNew2
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[1];
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>>", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>>", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("ClassSecondOld<String, CallSite<Func<CallSite, Object, Object, Object>>> _callSiteSetters", renamedItem.NameOld);
      Assert.AreEqual("ClassSecondOld<String, CallSite<Func<CallSite, Object, Object, Object>>> GenericFieldNew2", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld._callSiteSetters", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew2", renamedItem.NameNewPlain);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>> NsOld.ClassOld._callSiteSetters", renamedItem.NameOldFull);
      Assert.AreEqual("NsOld.ClassSecondOld<System.String, System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>> NsNew.ClassNew.GenericFieldNew2", renamedItem.NameNewFull);
    }

    [Test]
    public void RealNamingTest3()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyNullable = false;

      // [ModuleOld]System.Nullable`1<NsOld.ClassSecondOld>[] NsOld.ClassOld::GenericFieldOld3
      // -> GenericFieldNew3

      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[2];
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("System.Nullable<NsOld.ClassSecondOld>[]", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("System.Nullable<NsOld.ClassSecondOld>[]", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("Nullable<ClassSecondOld>[] GenericFieldOld3", renamedItem.NameOld);
      Assert.AreEqual("Nullable<ClassSecondOld>[] GenericFieldNew3", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericFieldOld3", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew3", renamedItem.NameNewPlain);
      Assert.AreEqual("System.Nullable<NsOld.ClassSecondOld>[] NsOld.ClassOld.GenericFieldOld3", renamedItem.NameOldFull);
      Assert.AreEqual("System.Nullable<NsOld.ClassSecondOld>[] NsNew.ClassNew.GenericFieldNew3", renamedItem.NameNewFull);
    }

    [Test]
    public void RealNamingTest3Simplfy()
    { 
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyNullable = true;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = true;

      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.Nullable`1<NsOld.ClassSecondOld>[] NsOld.ClassOld::GenericFieldOld3
      // -> GenericFieldNew3

      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[2];
      Assert.AreEqual(EntityType.Field, renamedItem.EntityType);
      Assert.IsNotNull(renamedItem.ResultType);
      Assert.AreEqual("NsOld.ClassSecondOld?[]", renamedItem.ResultType.NameOld.ToString());
      Assert.AreEqual("NsOld.ClassSecondOld?[]", renamedItem.ResultType.NameNew.ToString());
      Assert.IsNull(renamedItem.MethodParams);
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("ClassSecondOld?[] GenericFieldOld3", renamedItem.NameOld);
      Assert.AreEqual("ClassSecondOld?[] GenericFieldNew3", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericFieldOld3", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericFieldNew3", renamedItem.NameNewPlain);
      Assert.AreEqual("NsOld.ClassSecondOld?[] NsOld.ClassOld.GenericFieldOld3", renamedItem.NameOldFull);
      Assert.AreEqual("NsOld.ClassSecondOld?[] NsNew.ClassNew.GenericFieldNew3", renamedItem.NameNewFull);
    }

    [Test]
    public void RealNamingTest4()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyNullable = false;

      // [ModuleOld]NsOld.ClassOld::GenericMethodOld1(System.Nullable<System.Int64>[])
      // -> GenericMethodNew1

      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[3];
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("System.Nullable<System.Int64>[]", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("System.Nullable<System.Int64>[]", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("System.Nullable<System.Int64>[]", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void GenericMethodOld1(Nullable<Int64>[])", renamedItem.NameOld);
      Assert.AreEqual("void GenericMethodNew1(Nullable<Int64>[])", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericMethodOld1", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericMethodNew1", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(System.Nullable<System.Int64>[])", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew1(System.Nullable<System.Int64>[])", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(Nullable<Int64>[])", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew1(Nullable<Int64>[])", renamedItem.NameNewSimple);
      Assert.AreEqual("void GenericMethodOld1(Nullable<Int64>[]) → void GenericMethodNew1(Nullable<Int64>[])", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(System.Nullable<System.Int64>[]) → void NsNew.ClassNew.GenericMethodNew1(System.Nullable<System.Int64>[])", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(Nullable<Int64>[]) → void NsNew.ClassNew.GenericMethodNew1(Nullable<Int64>[])", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(Nullable<Int64>[]) → void NsNew.ClassNew.GenericMethodNew1(Nullable<Int64>[])", renamedItem.ToString());
    }

    [Test]
    public void RealNamingTest4Simplify()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyNullable = true;
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifyRef = true;

      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RealNamingTest.xml"));
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      
      // [ModuleOld]NsOld.ClassOld::GenericMethodOld1(System.Nullable<System.Int64>[])
      // -> GenericMethodNew1

      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[3];
      Assert.AreEqual(EntityType.Method, renamedItem.EntityType);
      Assert.IsNull(renamedItem.ResultType);
      Assert.IsNotNull(renamedItem.MethodParams);
      Assert.AreEqual(1, renamedItem.MethodParams.Count);
      Assert.AreEqual("long?[]", renamedItem.MethodParams[0].NameOld.ToString());
      Assert.AreEqual("long?[]", renamedItem.MethodParams[0].NameNew.ToString());
      Assert.AreEqual("long?[]", renamedItem.MethodParams[0].ToString());
      Assert.IsNotNull(renamedItem.Owner);
      Assert.AreEqual(renamedClass, renamedItem.Owner);
      Assert.AreEqual("ModuleOld", renamedItem.ModuleOld);
      Assert.AreEqual("ModuleNew", renamedItem.ModuleNew);
      Assert.AreEqual("void GenericMethodOld1(long?[])", renamedItem.NameOld);
      Assert.AreEqual("void GenericMethodNew1(long?[])", renamedItem.NameNew);
      Assert.AreEqual("NsOld.ClassOld.GenericMethodOld1", renamedItem.NameOldPlain);
      Assert.AreEqual("NsNew.ClassNew.GenericMethodNew1", renamedItem.NameNewPlain);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(long?[])", renamedItem.NameOldFull);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew1(long?[])", renamedItem.NameNewFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(long?[])", renamedItem.NameOldSimple);
      Assert.AreEqual("void NsNew.ClassNew.GenericMethodNew1(long?[])", renamedItem.NameNewSimple);
      Assert.AreEqual("void GenericMethodOld1(long?[]) → void GenericMethodNew1(long?[])", renamedItem.TransformName);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(long?[]) → void NsNew.ClassNew.GenericMethodNew1(long?[])", renamedItem.TransformNameFull);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(long?[]) → void NsNew.ClassNew.GenericMethodNew1(long?[])", renamedItem.TransformSimple);
      Assert.AreEqual("void NsOld.ClassOld.GenericMethodOld1(long?[]) → void NsNew.ClassNew.GenericMethodNew1(long?[])", renamedItem.ToString());
    }
  }
}