using NUnit.Framework;
using ObfuscarMappingParser;

namespace MappingParser.Tests
{
  [TestFixture]
  class NamingTest
  {
    [Test]
    public void ClassTest()
    {
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);

      // [ModuleOld]NsOld.ClassOld -> [ModuleNew]NsNew.ClassNew
      RenamedClass renamedClass = mapping.Classes[0];
      Assert.AreEqual(10, renamedClass.Items.Count);
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld NsOld.ClassOld::ClassFieldOld -> ClassFieldNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[0];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.Collections.Generic.Dictionary`2<System.String,System.String> NsOld.ClassOld::GenericFieldOld -> GenericFieldNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[1];
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
    public void PropertyTest()
    {
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.String NsOld.ClassOld::StringFieldOld -> StringFieldNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[2];
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
    public void StringResultMethodTest()
    {
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld NsOld.ClassOld::StringMethodOld() -> StringMethodNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[3];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::MethodWithParamOld(System.String) -> MethodWithParamNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[4];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld::GenericMethodOld(System.Collections.Generic.Dictionary`2&lt;System.String,NsOld.ClassOld&gt;) -> GenericMethodNew
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[5];
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
    public void TwoParamMethodTest()
    {
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]System.String NsOld.ClassOld::MethodWithParam2Old([ModuleOld]NsOld.ClassOld,System.String) -> MethodWithParam2New
      RenamedItem renamedItem = (RenamedItem)renamedClass.Items[6];
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
    public void SubclassTest()
    {
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // [ModuleOld]NsOld.ClassOld/SubclassOld -> [ModuleNew]NsNew.ClassNew/SubclassNew
      RenamedClass subclass = (RenamedClass)renamedClass.Items[7];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = (RenamedClass)renamedClass.Items[7];

      // [ModuleOld]System.String NsOld.ClassOld/SubclassOld::StringMethodOld() -> StringMethodNew
      RenamedItem renamedItem = (RenamedItem)subclass.Items[0];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = (RenamedClass)renamedClass.Items[7];

      // [ModuleOld]NsOld.ClassOld/SubclassOld::SubclassMethodOld(NsOld.ClassOld/SubclassOld) -> SubclassMethodNew
      RenamedItem renamedItem = (RenamedItem)subclass.Items[1];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];
      RenamedClass subclass = (RenamedClass)renamedClass.Items[7];

      // [ModuleOld]NsOld.ClassOld/SubclassOld NsOld.ClassOld/SubclassOld::SubclassResultMethodOld() -> SubclassResultMethodNew
      RenamedItem renamedItem = (RenamedItem)subclass.Items[2];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // skipped [ModuleOld]NsOld.ClassOld/SkippedSubclass1
      RenamedClass subclass = (RenamedClass)renamedClass.Items[8];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(1, mapping.Classes.Count);
      RenamedClass renamedClass = mapping.Classes[0];

      // skipped [ModuleOld]NsOld.ClassOld/SkippedSubclass2
      RenamedClass subclass = (RenamedClass)renamedClass.Items[9];
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
      Mapping mapping = new Mapping(@"Data\NamingTestMapping.xml");
      Assert.AreEqual(7, mapping.TotalMethodsCount);
      Assert.AreEqual(4, mapping.TotalClassesCount);
      Assert.AreEqual(3, mapping.TotalSubclassesCount);
      Assert.AreEqual(1, mapping.NamespacesCount);
      Assert.AreEqual(1, mapping.ObfuscatedNamespacesCount);
      Assert.AreEqual(1, mapping.ModulesCount);
      Assert.AreEqual(2, mapping.SkippedEntities);
    }
  }
}