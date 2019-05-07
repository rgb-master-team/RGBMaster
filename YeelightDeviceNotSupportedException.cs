using System;
using System.Runtime.Serialization;

namespace chroma_yeelight
{
    [Serializable]
    internal class YeelightDeviceNotSupportedException : Exception
    {
        public YeelightDeviceNotSupportedException()
        {
        }

        public YeelightDeviceNotSupportedException(string message) : base(message)
        {
        }

        public YeelightDeviceNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected YeelightDeviceNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}