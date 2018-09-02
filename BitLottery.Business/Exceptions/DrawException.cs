using System;
using System.Runtime.Serialization;

namespace BitLottery.Business.Exceptions
{
    [Serializable]
    public class DrawException : Exception
    {
        public DrawException()
        {
        }

        public DrawException(string message) : base(message)
        {
        }

        public DrawException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DrawException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}