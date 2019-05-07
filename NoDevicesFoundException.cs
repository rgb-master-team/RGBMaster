using System;
using System.Runtime.Serialization;

namespace chroma_yeelight
{
    [Serializable]
    internal class NoDevicesFoundException : Exception
    {
        public NoDevicesFoundException()
        {
        }

        public NoDevicesFoundException(string message) : base(message)
        {
        }

        public NoDevicesFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDevicesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}