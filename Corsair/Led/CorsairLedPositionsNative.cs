using System;
using System.Runtime.InteropServices;

namespace Corsair.Led
{
    /// <summary>
    /// Contains number of leds and array with their positions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairLedPositionsNative
    {
        /// <summary>
        /// Integer value. Number of elements in the following array
        /// </summary>
        public int numberOfLeds;

        /// <summary>
        /// Array of led positions.
        /// </summary>
        public IntPtr pLedPosition;
    }
}
