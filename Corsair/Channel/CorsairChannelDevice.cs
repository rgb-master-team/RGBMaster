using Corsair.Led;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about separate LED-device connected to the channel controlled by the DIY-device or cooler.
    /// </summary>
    public class CorsairChannelDevice
    {

	    /// <summary>
	    /// The native channel device info
	    /// </summary>
	    internal CorsairChannelDeviceInfoNative Native;

	    /// <summary>
	    /// Type of the LED-device
	    /// </summary>
	    public CorsairChannelDeviceType Type => Native.type;

	    /// <summary>
	    /// Contains number of leds and array with their positions
	    /// </summary>
	    public CorsairLedPosition[] LedPositions { get; private set; }

		/// <summary>
		/// Creates a instance of CorsairChannelDevice
		/// </summary>
		/// <param name="channelDeviceInfoNative">The native channel device info</param>
		internal CorsairChannelDevice(CorsairChannelDeviceInfoNative channelDeviceInfoNative)
        {
	        Native = channelDeviceInfoNative;

	        LedPositions = new CorsairLedPosition[Native.deviceLedCount];
		}

		public void Load(CorsairLedPosition[] ledPositions)
		{
			LedPositions = ledPositions;
		}
    }
}
