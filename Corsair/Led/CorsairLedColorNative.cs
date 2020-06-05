using System.Runtime.InteropServices;

namespace Corsair.Led
{
    /// <summary>
    /// Contains information about led and its color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairLedColorNative
    {
        /// <summary>
        /// Identifier of LED to set
        /// </summary>
        public CorsairLedId ledId;

        /// <summary>
        /// Red brightness [0..255]
        /// </summary>
        public int r;

        /// <summary>
        /// Green brightness [0..255]
        /// </summary>
        public int g;

        /// <summary>
        /// Blue brightness [0..255]
        /// </summary>
        public int b;
    }
}
