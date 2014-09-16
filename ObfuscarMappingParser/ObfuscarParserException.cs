using System;

namespace ObfuscarMappingParser
{
  class ObfuscarParserException: Exception
  {
    public ObfuscarParserException(string message, Exception innerException) : base(message, innerException) {}
  }
}
