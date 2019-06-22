using System;
using System.Runtime.InteropServices;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about separate channel of the DIY-device or cooler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairChannelNative
    {
        /// <summary>
        /// Total number of LEDs connected to the channel
        /// </summary>
        public int totalLedsCount;

        /// <summary>
        /// Number of LED-devices (fans, strips, etc.) connected to the channel which is controlled by the device
        /// </summary>
        public int devicesCount;

        /// <summary>
        /// Array containing information about each separate LED-device connected to the channel controlled by the device. Index of the LED-device in array is same as the index of the LED-device connected to the device.
        /// </summary>
        public IntPtr devices;
    }
}
