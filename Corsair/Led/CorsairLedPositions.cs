using System.Runtime.InteropServices;

namespace Corsair.Led
{
    /// <summary>
    /// Contains number of leds and array with their positions.
    /// </summary>
    public class CorsairLedPositions
    {
	    /// <summary>
	    /// The native led positions
	    /// </summary>
	    internal CorsairLedPositionsNative Native;

	    /// <summary>
        /// Array of led positions.
        /// </summary>
        public CorsairLedPosition[] LedPosition { get; set; }

        /// <summary>
        /// Creates a instance of CorsairLedPositions
        /// </summary>
        /// <param name="ledPositionsNative">The native led positions</param>
        internal CorsairLedPositions(CorsairLedPositionsNative ledPositionsNative)
        {
            Native = ledPositionsNative;

            LedPosition = new CorsairLedPosition[ledPositionsNative.numberOfLeds];

			var corsairLedPositionSize = Marshal.SizeOf(typeof(CorsairLedPositionNative));

            for (var ledIndex = 0; ledIndex < Native.numberOfLeds; ledIndex++)
            {
                var nativeLedPosition = Marshal.PtrToStructure<CorsairLedPositionNative>(Native.pLedPosition + corsairLedPositionSize * ledIndex);

                LedPosition[ledIndex] = new CorsairLedPosition(nativeLedPosition);
            }
        }
    }
}
