using System;

namespace OpenQuantumSafe
{
    /// <summary>
    /// An OQS exception.
    /// </summary>
    public class OQSException : Exception
    {
        public int InternalOQSStatus { private set; get; }

        public OQSException(string message) : base(message) { }
        public OQSException(int status_code) : base("OQS error")
        {
            InternalOQSStatus = status_code;
        }

        public OQSException(string message, Exception inner) : base(message, inner) { }

    }
}
