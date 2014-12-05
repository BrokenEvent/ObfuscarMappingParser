using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using ObfuscarMappingParser;

namespace MappingParser.Tests
{
  [TestFixture]
  class CommandLineTest
  {
    private StringBuilder outBuilder;
    private TextWriter outWriter;
    private MemoryStream inputStream;
    private TextReader inputReader;

    [SetUp]
    public void Setup()
    {
      outBuilder = new StringBuilder();
      outWriter = new StringWriter(outBuilder);
      Console.SetOut(outWriter);

      inputStream = new MemoryStream();
      inputReader = new StreamReader(inputStream);
      Console.SetIn(inputReader);

      CommandLine.doAttach = false;
    }

    public void Write(string value)
    {
      byte[] buffer = Encoding.ASCII.GetBytes(value);
      inputStream.Write(buffer, 0, buffer.Length);
      inputStream.Position = 0;
    }

    public void Expect(string expected)
    {
      Assert.AreEqual(expected + "\r\n", outBuilder.ToString());
    }

    [Test]
    public void TestProcesing()
    {
      string filename;
      Assert.AreEqual(true, CommandLine.ProcessCommandline(out filename, new string[] { @"c:\mapping.xml" }));
      Assert.AreEqual(@"c:\mapping.xml", filename);

      Assert.AreEqual(true, CommandLine.ProcessCommandline(out filename, new string[0]));
      Assert.IsNull(filename);
    }

    [Test]
    public void TestStacktrace()
    {
      string filename;

      Write(File.ReadAllText(@"Data\test1.txt") + "\r\n");

      Assert.AreEqual(false, CommandLine.ProcessCommandline(out filename, new string[] { @"Data\Mapping.xml", "-c" }));
      Expect(@"void AntiFreeze.Core.DataBase.GetReader(IDbCommand, string)
void AntiFreeze.Core.DataBase.GetReader(string)
void AntiFreeze.NET.NativeAPI.SetupNativeAPI()
void AntiFreeze.NET.MainForm.Startup()
void AntiFreeze.NET.MainForm.MainForm_Load(object, EventArgs)
");
    }

    [Test]
    public void TestHelp()
    {
      string filename;
      Assert.AreEqual(false, CommandLine.ProcessCommandline(out filename, new string[] { "/?" }));
      Expect(CommandLine.HELP_STRING);
    }
  }
}
