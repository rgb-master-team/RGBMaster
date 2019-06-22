using System;
using System.Runtime.Serialization;

namespace chroma_yeelight.Exceptions
{
    [Serializable]
    internal class DeviceCommandFailedException : Exception
    {
        public DeviceCommandFailedException()
        {
        }

        public DeviceCommandFailedException(string message) : base(message)
        {
        }

        public DeviceCommandFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeviceCommandFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}