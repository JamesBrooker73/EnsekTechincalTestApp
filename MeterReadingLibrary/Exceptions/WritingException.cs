using System.Runtime.Serialization;

namespace EnsekTechincalTest.Exceptions;

[Serializable]
public class WritingException : Exception
{
    public WritingException() { }

    public WritingException(string message) : base(message) { }

    public WritingException(string message, Exception innerException) : base(message, innerException) { }

    protected WritingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
