namespace Corsair.Led
{
    /// <summary>
    /// Contains information about led and its color.
    /// </summary>
    public class CorsairLedColor
    {
	    /// <summary>
	    /// The native led color
	    /// </summary>
	    internal CorsairLedColorNative Native;

		/// <summary>
		/// Identifier of LED to set
		/// </summary>
		public CorsairLedId LedId { get; set; }

        /// <summary>
        /// Red brightness [0..255]
        /// </summary>
        public int Red { get; set; }

        /// <summary>
        /// Green brightness [0..255]
        /// </summary>
        public int Green { get; set; }

        /// <summary>
        /// Blue brightness [0..255]
        /// </summary>
        public int Blue { get; set; }

        /// <summary>
        /// Creates a instance of CorsairLedColor
        /// </summary>
        public CorsairLedColor()
        {
	        Native = new CorsairLedColorNative();

			LedId = CorsairLedId.Invalid;
            Red = 0;
            Green = 0;
            Blue = 0;

            ApplyToNative();
        }

        /// <summary>
        /// Creates a instance of CorsairLedColor
        /// </summary>
        /// <param name="ledColorNative">The native led color</param>
        internal CorsairLedColor(CorsairLedColorNative ledColorNative)
        {
	        Native = ledColorNative ?? new CorsairLedColorNative();
			LedId = CorsairLedId.Invalid;
	        Red = 0;
	        Green = 0;
	        Blue = 0;

	        ApplyToManaged();
        }

        /// <summary>
        /// Applies changes on the object to the native instance
        /// </summary>
        internal void ApplyToNative()
        {
	        Native.ledId = LedId;
            Native.r = Red;
            Native.g = Green;
            Native.b = Blue;
        }

        /// <summary>
        /// Applies changes on the native instance to the object
        /// </summary>
        internal void ApplyToManaged()
        {
	        LedId = Native.ledId;
            Red = Native.r;
            Green = Native.g;
            Blue = Native.b;
        }
    }
}
