using System.Runtime.InteropServices;

namespace Corsair.Led
{
    /// <summary>
    /// Contains led id and position of led rectangle. Most of the keys are rectangular. In case if key is not rectangular (like Enter in ISO/UK layout) it returns the smallest rectangle that fully contains the key.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairLedPositionNative
    {
        /// <summary>
        /// Identifier of led
        /// </summary>
        public CorsairLedId ledId;

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double top;

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double left;

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double height;

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double width;
    }
}
