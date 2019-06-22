using System;
using System.Runtime.InteropServices;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about channels of the DIY-devices or cooler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairChannelsInfoNative
    {
        /// <summary>
        /// Number of channels controlled by the device
        /// </summary>
        public int channelsCount;

        /// <summary>
        /// Array containing information about each separate channel of the device. Index of the channel in the array is same as index of the channel on the device.
        /// </summary>
        public IntPtr channels;
    }
}
