using System;

namespace ObfuscarMappingParser.Engine
{
  class ObfuscarParserException: Exception
  {
    public ObfuscarParserException(string message) : base(message) {}
    public ObfuscarParserException(string message, string textData) : base($"{message}:\n{textData}") {}
    public ObfuscarParserException(string message, Exception innerException, string textData) : base($"{message}:\n{textData}", innerException) {}
  }
}
