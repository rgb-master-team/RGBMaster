using System.Runtime.InteropServices;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about separate LED-device connected to the channel controlled by the DIY-device or cooler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairChannelDeviceInfoNative
    {
        /// <summary>
        /// Type of the LED-device
        /// </summary>
        public CorsairChannelDeviceType type;

        /// <summary>
        /// Number of LEDs controlled by LED-device.
        /// </summary>
        public int deviceLedCount;
    }
}
