using System;
using System.Runtime.Serialization;

namespace Yeelight
{
    [Serializable]
    internal class YeelightDeviceNotSupportedException : Exception
    {
        public YeelightDeviceNotSupportedException() : this(null)
        {
        }

        public YeelightDeviceNotSupportedException(Exception innerException) : base("This device does not support music mode - which is essential for syncing music to colors.", innerException)
        {
        }
    }
}