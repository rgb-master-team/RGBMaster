using System;
using System.Runtime.Serialization;

namespace Logitech
{
    [Serializable]
    internal class LogitechInitFailedException : Exception
    {
        public LogitechInitFailedException() : base("Failed to initialize Logitech SDK")
        {
        }
    }
}