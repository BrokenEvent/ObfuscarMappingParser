using System;
using BrokenEvent.Shared.Rest;

namespace ObfuscarMappingParser
{
  class ObfuscarParserException: ReportedException
  {
    public ObfuscarParserException(string message) : base(message) {}
    public ObfuscarParserException(string message, string textData) : base(message, textData) {}
    public ObfuscarParserException(string message, byte[] binaryData) : base(message, binaryData) {}
    public ObfuscarParserException(string message, Exception innerException) : base(message, innerException) {}
    public ObfuscarParserException(string message, Exception innerException, string textData) : base(message, innerException, textData) {}
    public ObfuscarParserException(string message, Exception innerException, byte[] binaryData) : base(message, innerException, binaryData) {}
  }
}
