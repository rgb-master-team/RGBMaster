using System;
using System.Runtime.InteropServices;

namespace Corsair.Protocol
{
    /// <summary>
    /// Contains information about SDK and CUE versions.
    /// </summary>
    public struct CorsairProtocolDetails
    {
	    /// <summary>
	    /// The native protocol details
	    /// </summary>
	    internal CorsairProtocolDetailsNative Native;

		/// <summary>
		/// Null-terminated string containing version of SDK (like “1.0.0.1”). Always contains valid value even if there was no CUE found
		/// </summary>
		public string SdkVersion { get; set; }

        /// <summary>
        /// Null-terminated string containing version of CUE (like “1.0.0.1”) or NULL if CUE was not found
        /// </summary>
        public string ServerVersion { get; set; }

        /// <summary>
        /// Integer number that specifies version of protocol that is implemented by current SDK. Numbering starts from 1. Always contains valid value even if there was no CUE found
        /// </summary>
        public int SdkProtocolVersion
        {
	        get => Native.sdkProtocolVersion;
	        set => Native.sdkProtocolVersion = value;
        }

        /// <summary>
        /// Integer number that specifies version of protocol that is implemented by CUE. Numbering starts from 1. If CUE was not found then this value will be 0
        /// </summary>
        public int ServerProtocolVersion
        {
	        get => Native.serverProtocolVersion;
	        set => Native.serverProtocolVersion = value;
        }

        /// <summary>
		/// Boolean value that specifies if there were breaking changes between version of protocol implemented by server and client.
		/// </summary>
		public bool BreakingChanges { get; set; }

		/// <summary>
        /// Creates a instance of CorsairProtocolDetails
        /// </summary>
        /// <param name="protocolDetailsNative">The native protocol details</param>
        internal CorsairProtocolDetails(CorsairProtocolDetailsNative protocolDetailsNative)
        {
	        Native = protocolDetailsNative;

            SdkVersion = Marshal.PtrToStringAnsi(Native.sdkVersion);
            ServerVersion = Marshal.PtrToStringAnsi(Native.serverVersion);
            BreakingChanges = Convert.ToBoolean(Native.breakingChanges);
        }
    }
}
