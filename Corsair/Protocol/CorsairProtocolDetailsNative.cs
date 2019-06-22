using System;
using System.Runtime.InteropServices;

namespace Corsair.Protocol
{
    /// <summary>
    /// Contains information about SDK and CUE versions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CorsairProtocolDetailsNative
    {
        /// <summary>
        /// Null-terminated string containing version of SDK (like “1.0.0.1”). Always contains valid value even if there was no CUE found
        /// </summary>
        public IntPtr sdkVersion;

        /// <summary>
        /// Null-terminated string containing version of CUE (like “1.0.0.1”) or NULL if CUE was not found
        /// </summary>
        public IntPtr serverVersion;

        /// <summary>
        /// Integer number that specifies version of protocol that is implemented by current SDK. Numbering starts from 1. Always contains valid value even if there was no CUE found
        /// </summary>
        public int sdkProtocolVersion;

        /// <summary>
        /// Integer number that specifies version of protocol that is implemented by CUE. Numbering starts from 1. If CUE was not found then this value will be 0
        /// </summary>
        public int serverProtocolVersion;

        /// <summary>
        /// Boolean value that specifies if there were breaking changes between version of protocol implemented by server and client.
        /// </summary>
        public byte breakingChanges;
    }
}
