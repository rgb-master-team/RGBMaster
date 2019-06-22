using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Corsair.Led;

namespace Corsair.Channel
{
    /// <summary>
    /// Contains information about channels of the DIY-devices or cooler.
    /// </summary>
    public class CorsairChannels
    {
	    /// <summary>
	    /// The native channels info
	    /// </summary>
	    internal CorsairChannelsInfoNative Native;

	    /// <summary>
        /// Array containing information about each separate channel of the device. Index of the channel in the array is same as index of the channel on the device.
        /// </summary>
        public List<CorsairChannel> Channels { get; }

        /// <summary>
        /// Creates a instance of CorsairChannels
        /// </summary>
        /// <param name="channelsInfoNative">The native channels info</param>
        internal CorsairChannels(CorsairChannelsInfoNative channelsInfoNative)
        {
            Native = channelsInfoNative;

            Channels = new List<CorsairChannel>();

            var corsairChannelInfoSize = Marshal.SizeOf(typeof(CorsairChannelNative));

            for (var channelIndex = 0; channelIndex < Native.channelsCount; channelIndex++)
            {
                var nativeChannelInfo = Marshal.PtrToStructure<CorsairChannelNative>(Native.channels + corsairChannelInfoSize * channelIndex);

                Channels.Add(new CorsairChannel(nativeChannelInfo));
            }
        }

        internal void Load(CorsairLedPosition[] ledPositions)
        {
	        var takenLedsCounter = 0;

	        foreach (var channel in Channels)
	        {
		        channel.Load(ledPositions.Skip(takenLedsCounter).Take(channel.LedPositions.Length).ToArray());

		        takenLedsCounter += channel.LedPositions.Length;
	        }
        }
    }
}
