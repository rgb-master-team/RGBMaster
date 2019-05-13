using System;
using System.Runtime.Serialization;

namespace chroma_yeelight
{
    [Serializable]
    internal class AudioCaptureAccessDeniedException : Exception
    {
        public AudioCaptureAccessDeniedException()
        {
        }

        public AudioCaptureAccessDeniedException(string message) : base(message)
        {
        }

        public AudioCaptureAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AudioCaptureAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}