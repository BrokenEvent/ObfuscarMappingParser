﻿using System.Collections.Generic;

using NUnit.Framework;
using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;

namespace MappingParser.Tests
{
  [TestFixture]
  class CrashlogTest
  {
    private static void AssertResult(List<SearchResults> list, int index, string module, string name, string nameSimple, string nameFull)
    {
      SearchResults results = list[index];
      INamedEntity result = results.SingleResult;
      Assert.IsTrue(result is RenamedBase, "Is RenamedBase");
      Assert.IsNotNull(result, "Have result");
      Assert.IsTrue(results.IsSingleResult, "Single result");
      Assert.AreEqual(SearchResultMessage.Normal, results.Message, "Result type");
      Assert.AreEqual(module, result.Module, "Module");
      Assert.AreEqual(nameFull, result.NameFull, "NameFull");
      Assert.AreEqual(nameSimple, result.NameSimple, "NameSimple");
      Assert.AreEqual(name, result.NameShort, "NameShort");
      Assert.AreEqual(nameSimple, results.ToString(), "results.ToString");
      Assert.AreEqual(name, results.ToString(OutputType.Short), "ToString(Short)");
      Assert.AreEqual(nameSimple, results.ToString(OutputType.Simple), "ToString(Simple)");
      Assert.AreEqual(nameFull, results.ToString(OutputType.Full), "ToString(Full)");
    }

    private static void AssertResult(List<SearchResults> list, int index, string name, string nameSimple, string nameFull)
    {
      SearchResults results = list[index];
      INamedEntity result = results.SingleResult;
      Assert.IsNull(result, "Have no result");
      Assert.IsFalse(results.IsSingleResult, "Not a single result");
      Assert.AreEqual(SearchResultMessage.Failed, results.Message, "Result type");
      Assert.AreEqual(nameSimple, results.ToString(), "results.ToString");
      Assert.AreEqual(name, results.ToString(OutputType.Short), "ToString(Short)");
      Assert.AreEqual(nameSimple, results.ToString(OutputType.Simple), "ToString(Simple)");
      Assert.AreEqual(nameFull, results.ToString(OutputType.Full), "ToString(Full)");
    }

    private static void AssertSubstitution(List<SearchResults> list, int index, string name, string nameSimple, string nameFull)
    {
      SearchResults results = list[index];
      INamedEntity result = results.SingleResult;
      Assert.IsFalse(result is RenamedBase, "Is NOT RenamedBase (and is just some Entity)");
      Assert.IsNotNull(result, "Have result");
      Assert.IsTrue(results.IsSingleResult, "Single result");
      Assert.AreEqual(SearchResultMessage.Substitution, results.Message, "Result type");
      Assert.AreEqual(nameFull, result.NameFull, "NameFull");
      Assert.AreEqual(nameSimple, result.NameSimple, "NameSimple");
      Assert.AreEqual(name, result.NameShort, "NameShort");
      Assert.AreEqual(nameSimple, results.ToString(), "results.ToString");
      Assert.AreEqual(name, results.ToString(OutputType.Short), "ToString(Short)");
      Assert.AreEqual(nameSimple, results.ToString(OutputType.Simple), "ToString(Simple)");
      Assert.AreEqual(nameFull, results.ToString(OutputType.Full), "ToString(Full)");
    }

    private static void AssertAmbigous(List<SearchResults> list, int index, string name, string nameSimple, string nameFull)
    {
      const string POSTFIX = "/* ambiguous */";
      SearchResults results = list[index];
      Assert.IsFalse(results.IsSingleResult, "Not a single result");
      Assert.IsNotNull(results.Results, "Not null result list");
      Assert.Greater(results.Results.Count, 0, "More than zero elements in result");
      Assert.AreEqual(SearchResultMessage.Ambiguous, results.Message, "Result type");
      Assert.AreEqual(nameSimple + POSTFIX, results.ToString(), "results.ToString");
      Assert.AreEqual(name + POSTFIX, results.ToString(OutputType.Short), "ToString(Short)");
      Assert.AreEqual(nameSimple + POSTFIX, results.ToString(OutputType.Simple), "ToString(Simple)");
      Assert.AreEqual(nameFull + POSTFIX, results.ToString(OutputType.Full), "ToString(Full)");
    }

    [SetUp]
    public void SetUp()
    {
      ParserConfigs.Instance = new ParserConfigsImpl();
    }

    [Test]
    public void Test1()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test1.txt"));

      Assert.AreEqual(5, results.Count);

      AssertResult(
          results,
          0,
          "AntiFreeze.Core",
          "void GetReader(IDbCommand, String)",
          "void AntiFreeze.Core.DataBase.GetReader(IDbCommand, String)",
          "void AntiFreeze.Core.DataBase.GetReader(System.Data.IDbCommand, System.String)"
        );

      AssertResult(
          results,
          1,
          "AntiFreeze.Core",
          "void GetReader(String)",
          "void AntiFreeze.Core.DataBase.GetReader(String)",
          "void AntiFreeze.Core.DataBase.GetReader(System.String)"
        );

      AssertResult(
          results,
          2,
          "AntiFreeze.NET",
          "void SetupNativeAPI()",
          "void AntiFreeze.NET.NativeAPI.SetupNativeAPI()",
          "void AntiFreeze.NET.NativeAPI.SetupNativeAPI()"
        );

      AssertResult(
          results,
          3,
          "AntiFreeze.NET",
          "void Startup()",
          "void AntiFreeze.NET.MainForm.Startup()",
          "void AntiFreeze.NET.MainForm.Startup()"
        );

      AssertResult(
          results,
          4,
          "AntiFreeze.NET",
          "void MainForm_Load(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.MainForm_Load(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.MainForm_Load(System.Object, System.EventArgs)"
        );
    }

    [Test]
    public void Test2()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test2.txt"));

      Assert.AreEqual(1, results.Count);

      AssertResult(
          results,
          0,
          "AntiFreeze.NET",
          "void DwmSetWindowAttribute(IntPtr, Int32, ref Int32, Int32)",
          "void BrokenEvent.Shared.AeroPeekModifier.DwmSetWindowAttribute(IntPtr, Int32, ref Int32, Int32)",
          "void BrokenEvent.Shared.AeroPeekModifier.DwmSetWindowAttribute(System.IntPtr, System.Int32, ref System.Int32, System.Int32)"
        );
    }

    [Test]
    public void Test3()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test3.txt"));

      Assert.AreEqual(7, results.Count);

      AssertResult(
          results,
          0,
          "void StartWithShellExecuteEx(ProcessStartInfo)",
          "void System.Diagnostics.Process.StartWithShellExecuteEx(ProcessStartInfo)",
          "void System.Diagnostics.Process.StartWithShellExecuteEx(ProcessStartInfo)"
        );

      AssertResult(
          results,
          1,
          "void Start()",
          "void System.Diagnostics.Process.Start()",
          "void System.Diagnostics.Process.Start()"
        );

      AssertResult(
          results,
          2,
          "void Start(ProcessStartInfo)",
          "void System.Diagnostics.Process.Start(ProcessStartInfo)",
          "void System.Diagnostics.Process.Start(ProcessStartInfo)"
        );

      AssertResult(
          results,
          3,
          "void Start(String)",
          "void System.Diagnostics.Process.Start(String)",
          "void System.Diagnostics.Process.Start(String)"
        );

      AssertResult(
          results,
          4,
          "AntiFreeze.NET",
          "void llblSupportEmail_LinkClicked(Object, LinkLabelLinkClickedEventArgs)",
          "void AntiFreeze.NET.AboutForm.llblSupportEmail_LinkClicked(Object, LinkLabelLinkClickedEventArgs)",
          "void AntiFreeze.NET.AboutForm.llblSupportEmail_LinkClicked(System.Object, System.Windows.Forms.LinkLabelLinkClickedEventArgs)"
        );

      AssertResult(
          results,
          5,
          "void OnLinkClicked(LinkLabelLinkClickedEventArgs)",
          "void System.Windows.Forms.LinkLabel.OnLinkClicked(LinkLabelLinkClickedEventArgs)",
          "void System.Windows.Forms.LinkLabel.OnLinkClicked(LinkLabelLinkClickedEventArgs)"
        );

      AssertResult(
          results,
          6,
          "void OnMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.LinkLabel.OnMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.LinkLabel.OnMouseUp(MouseEventArgs)"
        );
    }

    [Test]
    public void Test4()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test4.txt"));
      Assert.AreEqual(9, results.Count);

      AssertResult(
          results,
          0,
          "void WaitForWaitHandle(WaitHandle)",
          "void System.Windows.Forms.Control.WaitForWaitHandle(WaitHandle)",
          "void System.Windows.Forms.Control.WaitForWaitHandle(WaitHandle)"
        );

      AssertResult(
          results,
          1,
          "void MarshaledInvoke(Control, Delegate, Object[], Boolean)",
          "void System.Windows.Forms.Control.MarshaledInvoke(Control, Delegate, Object[], Boolean)",
          "void System.Windows.Forms.Control.MarshaledInvoke(Control, Delegate, Object[], Boolean)"
        );

      AssertResult(
          results,
          2,
          "void Invoke(Delegate, Object[])",
          "void System.Windows.Forms.Control.Invoke(Delegate, Object[])",
          "void System.Windows.Forms.Control.Invoke(Delegate, Object[])"
        );

      AssertResult(
          results,
          3,
          "void Invoke(Delegate)",
          "void System.Windows.Forms.Control.Invoke(Delegate)",
          "void System.Windows.Forms.Control.Invoke(Delegate)"
        );

      AssertResult(
          results,
          4,
          "AntiFreeze.NET",
          "void RefreshPanel(IPanel)",
          "void AntiFreeze.NET.WorkPanel.RefreshPanel(IPanel)",
          "void AntiFreeze.NET.WorkPanel.RefreshPanel(AntiFreeze.Interfaces.Core.IPanel)"
        );

      /*ResultTestHelperOk(
          results,
          5,
          "AntiFreeze.Basic",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(Single, Single, Int64, Int64)",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(Single, Single, Int64, Int64)",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(Single, Single, Int64, Int64)"
        );

      ResultTestHelperOk(
          results,
          6,
          "AntiFreeze.Basic",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(TimeSpan)",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(TimeSpan)",
          "void AntiFreeze.Basic.TrafficMonitorThread.A(TimeSpan)"
        );

      ResultTestHelper(
          results,
          7,
          "void AntiFreeze.Basic.TrafficMonitorThread.l()",
          "void AntiFreeze.Basic.TrafficMonitorThread.l()",
          "void AntiFreeze.Basic.TrafficMonitorThread.l()"
        );*/ // test will not pass, this is a method of superclass but app have no idea about it for now

      AssertResult(
          results,
          8,
          "AntiFreeze.Core",
          "void StartThread()",
          "void AntiFreeze.Core.AbstractWorkingThread.StartThread()",
          "void AntiFreeze.Core.AbstractWorkingThread.StartThread()"
        );
    }

    [Test]
    public void Test5()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test5.txt"));
      Assert.AreEqual(3, results.Count);

      AssertResult(
          results,
          0,
          "AntiFreeze.Basic",
          "void DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(System.Drawing.Rectangle, System.Single, System.Single, System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          1,
          "AntiFreeze.Core",
          "void Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          2,
          "AntiFreeze.NET",
          "void OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(System.Windows.Forms.PaintEventArgs)"
        );
    }

    [Test]
    public void Test5Ru()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test5_ru.txt"));
      Assert.AreEqual(3, results.Count);

      AssertResult(
          results,
          0,
          "AntiFreeze.Basic",
          "void DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(System.Drawing.Rectangle, System.Single, System.Single, System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          1,
          "AntiFreeze.Core",
          "void Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          2,
          "AntiFreeze.NET",
          "void OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(System.Windows.Forms.PaintEventArgs)"
        );
    }

    [Test]
    public void Test5Nl()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test5_nl.txt"));
      Assert.AreEqual(3, results.Count);

      AssertResult(
          results,
          0,
          "AntiFreeze.Basic",
          "void DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(Rectangle, Single, Single, Graphics)",
          "void AntiFreeze.Basic.PingResultPanel.DrawGraph(System.Drawing.Rectangle, System.Single, System.Single, System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          1,
          "AntiFreeze.Core",
          "void Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(Graphics)",
          "void AntiFreeze.Core.GraphResultPanel.Draw(System.Drawing.Graphics)"
        );

      AssertResult(
          results,
          2,
          "AntiFreeze.NET",
          "void OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(PaintEventArgs)",
          "void AntiFreeze.NET.WorkPanel.OnPaint(System.Windows.Forms.PaintEventArgs)"
        );
    }

    [Test]
    public void Test1Broken()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test1_broken.txt"), false);

      Assert.AreEqual(5, results.Count);

      AssertAmbigous(
          results,
          0,
          "void get_IsNewDataBase()",
          "void AntiFreeze.Core.DataBase.get_IsNewDataBase()",
          "void AntiFreeze.Core.DataBase.get_IsNewDataBase()"
        );

      AssertAmbigous(
          results,
          1,
          "void get_Command()",
          "void AntiFreeze.Core.DataBase.get_Command()",
          "void AntiFreeze.Core.DataBase.get_Command()"
        );

      AssertAmbigous(
          results,
          2,
          "void SetupNativeAPI()",
          "void AntiFreeze.NET.NativeAPI.SetupNativeAPI()",
          "void AntiFreeze.NET.NativeAPI.SetupNativeAPI()"
        );

      AssertAmbigous(
          results,
          3,
          "void ThreadActionItem_Click(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.ThreadActionItem_Click(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.ThreadActionItem_Click(System.Object, System.EventArgs)"
        );

      AssertResult(
          results,
          4,
          "AntiFreeze.NET",
          "void MainForm_Load(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.MainForm_Load(Object, EventArgs)",
          "void AntiFreeze.NET.MainForm.MainForm_Load(System.Object, System.EventArgs)"
        );
    }

    [Test]
    public void Test6()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping_Parser.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test6.txt"));

      Assert.AreEqual(6, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(ObfuscarMappingParser.MainForm, String, String)"
        );

      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void miStacktrace_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.MainForm.miStacktrace_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.MainForm.miStacktrace_Click(System.Object, System.EventArgs)"
        );

      AssertResult(
          results,
          2,
          "void RaiseEvent(Object, EventArgs)",
          "void System.Windows.Forms.ToolStripItem.RaiseEvent(Object, EventArgs)",
          "void System.Windows.Forms.ToolStripItem.RaiseEvent(Object, EventArgs)"
        );

      AssertResult(
          results,
          3,
          "void OnClick(EventArgs)",
          "void System.Windows.Forms.ToolStripMenuItem.OnClick(EventArgs)",
          "void System.Windows.Forms.ToolStripMenuItem.OnClick(EventArgs)"
        );

      AssertResult(
          results,
          4,
          "void HandleClick(EventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleClick(EventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleClick(EventArgs)"
        );

      AssertResult(
          results,
          5,
          "void HandleMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleMouseUp(MouseEventArgs)"
        );
    }

    [Test]
    public void Test6Simpify()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\Mapping_Parser.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\test6.txt"));

      Assert.AreEqual(6, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(MainForm, string, string)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(MainForm, string, string)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(ObfuscarMappingParser.MainForm, string, string)"
        );

      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void miStacktrace_Click(object, EventArgs)",
          "void ObfuscarMappingParser.MainForm.miStacktrace_Click(object, EventArgs)",
          "void ObfuscarMappingParser.MainForm.miStacktrace_Click(object, System.EventArgs)"
        );

      AssertResult(
          results,
          2,
          "void RaiseEvent(object, EventArgs)",
          "void System.Windows.Forms.ToolStripItem.RaiseEvent(object, EventArgs)",
          "void System.Windows.Forms.ToolStripItem.RaiseEvent(object, EventArgs)"
        );

      AssertResult(
          results,
          3,
          "void OnClick(EventArgs)",
          "void System.Windows.Forms.ToolStripMenuItem.OnClick(EventArgs)",
          "void System.Windows.Forms.ToolStripMenuItem.OnClick(EventArgs)"
        );

      AssertResult(
          results,
          4,
          "void HandleClick(EventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleClick(EventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleClick(EventArgs)"
        );

      AssertResult(
          results,
          5,
          "void HandleMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleMouseUp(MouseEventArgs)",
          "void System.Windows.Forms.ToolStripItem.HandleMouseUp(MouseEventArgs)"
        );
    }

    [Test]
    public void UnicodeTest1()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\unicode_mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\unicode_test1.txt"));

      Assert.AreEqual(5, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)"
        );
      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(System.String, System.Boolean)"
        );
      AssertResult(
          results,
          2,
          "ObfuscarMappingParser",
          "void ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(System.String)"
        );
      AssertResult(
          results,
          3,
          "ObfuscarMappingParser",
          "void ProcessCrashlogText(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(System.String)"
        );
      AssertResult(
          results,
          4,
          "ObfuscarMappingParser",
          "void btnProcess_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(System.Object, System.EventArgs)"
        );
    }

    [Test]
    public void UnicodeTest1Simplify()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = true;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\unicode_mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\unicode_test1.txt"));

      Assert.AreEqual(5, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(string)",
          "ObfuscarMappingParser.Entity.ctor(string)",
          "ObfuscarMappingParser.Entity.ctor(string)"
        );
      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void Search(string, bool)",
          "void ObfuscarMappingParser.Mapping.Search(string, bool)",
          "void ObfuscarMappingParser.Mapping.Search(string, bool)"
        );
      AssertResult(
          results,
          2,
          "ObfuscarMappingParser",
          "void ProcessCrashlog(string)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(string)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(string)"
        );
      AssertResult(
          results,
          3,
          "ObfuscarMappingParser",
          "void ProcessCrashlogText(string)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(string)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(string)"
        );
      AssertResult(
          results,
          4,
          "ObfuscarMappingParser",
          "void btnProcess_Click(object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(object, System.EventArgs)"
        );
    }

    [Test]
    public void UnicodeTest2()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\unicode_mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\unicode_test2.txt"));

      Assert.AreEqual(4, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)"
        );
      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(System.String, System.Boolean)"
        );
      AssertResult(
          results,
          2,
          "ObfuscarMappingParser",
          "void ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(System.String)"
        );
      AssertSubstitution(
          results,
          3,
          "ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(ObfuscarMappingParser.MainForm, String, String)"
        );
    }

    [Test]
    public void KoreanTest1()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\korean_mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\korean_test1.txt"));

      Assert.AreEqual(5, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)"
        );
      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(System.String, System.Boolean)"
        );
      AssertResult(
          results,
          2,
          "ObfuscarMappingParser",
          "void ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(System.String)"
        );
      AssertResult(
          results,
          3,
          "ObfuscarMappingParser",
          "void ProcessCrashlogText(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlogText(System.String)"
        );
      AssertResult(
          results,
          4,
          "ObfuscarMappingParser",
          "void btnProcess_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(Object, EventArgs)",
          "void ObfuscarMappingParser.CrashLog.btnProcess_Click(System.Object, System.EventArgs)"
        );
    }

    [Test]
    public void KoreanTest2()
    {
      ((ParserConfigsImpl)ParserConfigs.Instance).SimplifySystemNames = false;
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\korean_mapping.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\korean_test2.txt"));

      Assert.AreEqual(4, results.Count);

      AssertSubstitution(
          results,
          0,
          "ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)",
          "ObfuscarMappingParser.Entity.ctor(String)"
        );
      AssertResult(
          results,
          1,
          "ObfuscarMappingParser",
          "void Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(String, Boolean)",
          "void ObfuscarMappingParser.Mapping.Search(System.String, System.Boolean)"
        );
      AssertResult(
          results,
          2,
          "ObfuscarMappingParser",
          "void ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(String)",
          "void ObfuscarMappingParser.Mapping.ProcessCrashlog(System.String)"
        );
      AssertSubstitution(
          results,
          3,
          "ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(MainForm, String, String)",
          "ObfuscarMappingParser.StacktraceAnalyerForm.ctor(ObfuscarMappingParser.MainForm, String, String)"
        );
    }

    [Test]
    public void RenamedInSkipped()
    {
      Mapping mapping = new Mapping(TestHelper.TranslatePath(@"Data\RenamedInSkipped.xml"));
      List<SearchResults> results = mapping.ProcessCrashlog(TestHelper.ReadAllText(@"Data\RenamedInSkipped.txt"));
      Assert.AreEqual(4, results.Count);

      AssertResult(
          results,
          0,
          "lib",
          "void MakeUpperCase(string)",
          "void lib.SomethingFancy.MakeUpperCase(string)",
          "void lib.SomethingFancy.MakeUpperCase(string)"
        );

      AssertResult(
          results,
          1,
          "lib",
          "void SomeSecretStuff(string)",
          "void lib.SomethingFancy.SomeSecretStuff(string)",
          "void lib.SomethingFancy.SomeSecretStuff(string)"
        );
    }
  }
}
