using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ObfuscarMappingParser
{
  internal static class CommandLine
  {
    [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "AttachConsole")]
    private static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;
    internal static bool doAttach = true; // test purposes only!

    internal const string HELP_STRING = @"Usage:
ObfuscarMappingParser.exe - open launcher GUI
ObfuscarMappingParser.exe %filename% - open GUI with file %filename%
ObfuscarMappingParser.exe \? = help
ObfuscarMappingParser.exe %filename% -c - open mapping, read stacktrace from standart input and write processed stacktrace to the standart output";

    public static bool ProcessCommandline(out string filename, string[] args)
    {
      if (args.Length == 0)
      {
        filename = null;
        return true;
      }

      if (args[0] == "/?" || args[0] == "-?")
      {
        filename = null;
        WriteHelp();
        return false;       
      }

      filename = args[0];

      if (args.Length == 1)
        return true;

      switch (args[1])
      {
        case "-c":
          Process(filename);
          return false;
      }

      return true;
    }

    private static void Process(string filename)
    {
      if (doAttach && !AttachConsole(ATTACH_PARENT_PROCESS))
        return;

      Mapping mapping = new Mapping(filename);
      StringBuilder sb = new StringBuilder();
      string s;
      while (!string.IsNullOrEmpty(s = Console.ReadLine()))
        sb.AppendLine(s);

      Console.WriteLine(mapping.ProcessCrashlogText(sb.ToString()));
    }

    private static void WriteHelp()
    {
      Console.WriteLine(HELP_STRING);
    }
  }
}
