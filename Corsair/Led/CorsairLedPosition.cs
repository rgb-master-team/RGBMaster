namespace Corsair.Led
{
    /// <summary>
    /// Contains led id and position of led rectangle. Most of the keys are rectangular. In case if key is not rectangular (like Enter in ISO/UK layout) it returns the smallest rectangle that fully contains the key.
    /// </summary>
    public class CorsairLedPosition
    {
	    /// <summary>
	    /// The native led position
	    /// </summary>
	    internal CorsairLedPositionNative Native;

		/// <summary>
		/// Identifier of led
		/// </summary>
		public CorsairLedId LedId
		{
			get => Native.ledId;
			set => Native.ledId = value;
		}

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double Top
        {
	        get => Native.top;
	        set => Native.top = value;
        }

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double Left
        {
	        get => Native.left;
	        set => Native.left = value;
        }

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double Height
        {
	        get => Native.height;
	        set => Native.height = value;
        }

        /// <summary>
        /// For keyboards, mice, mousemats, headset stands and memory modules values are in mm, for DIY-devices, headsets and coolers values are in logical units.
        /// </summary>
        public double Width
        {
	        get => Native.width;
	        set => Native.width = value;
        }

        /// <summary>
        /// Creates a instance of CorsairLedPosition
        /// </summary>
        /// <param name="ledPositionNative">The native led position</param>
        internal CorsairLedPosition(CorsairLedPositionNative ledPositionNative)
        {
            Native = ledPositionNative;
        }
    }
}
