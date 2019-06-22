using System;
using System.Runtime.Serialization;

namespace chroma_yeelight.Exceptions
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

        public AudioCaptureAccessDeniedException(Exception innerException) : base("Make sure there are no background softwares running in your pc that are capturing background activity! (including MOBO software, Realtek HD, Asus Sonic etc.)", innerException)
        {
        }

        protected AudioCaptureAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}