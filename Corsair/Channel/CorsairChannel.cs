using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Corsair.Led;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about separate channel of the DIY-device or cooler.
    /// </summary>
    public class CorsairChannel
    {
	    /// <summary>
	    /// The native channel info
	    /// </summary>
	    internal CorsairChannelNative Native { get; set; }

        /// <summary>
        /// Array containing information about each separate LED-device connected to the channel controlled by the device. Index of the LED-device in array is same as the index of the LED-device connected to the device.
        /// </summary>
        public List<CorsairChannelDevice> Devices { get; }

        /// <summary>
        /// Contains number of leds and array with their positions of all the devices.
        /// </summary>
        public CorsairLedPosition[] LedPositions { get; private set; }

		/// <summary>
		/// Creates a instance of CorsairChannel
		/// </summary>
		/// <param name="channelNative">The native channel info</param>
		internal CorsairChannel(CorsairChannelNative channelNative)
        {
	        Native = channelNative;

            LedPositions = new CorsairLedPosition[Native.totalLedsCount];
			Devices = new List<CorsairChannelDevice>();

            var corsairChannelDeviceInfoSize = Marshal.SizeOf(typeof(CorsairChannelDeviceInfoNative));

            for (var deviceIndex = 0; deviceIndex < Native.devicesCount; deviceIndex++)
            {
                var nativeChannelDeviceInfo = Marshal.PtrToStructure<CorsairChannelDeviceInfoNative>(Native.devices + corsairChannelDeviceInfoSize * deviceIndex);

                Devices.Add(new CorsairChannelDevice(nativeChannelDeviceInfo));
            }
        }

		internal void Load(CorsairLedPosition[] ledPositions)
		{
			LedPositions = ledPositions;

			var takenLedsCounter = 0;

			foreach (var channel in Devices)
			{
				channel.Load(ledPositions.Skip(takenLedsCounter).Take(channel.LedPositions.Length).ToArray());

				takenLedsCounter += channel.LedPositions.Length;
			}
		}
    }
}
