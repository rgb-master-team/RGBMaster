using System;
using System.Runtime.InteropServices;
using Corsair.Channel;
using Corsair.Layout;

namespace Corsair.Device
{
    /// <summary>
    /// Contains information about device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairDeviceNative
    {
        /// <summary>
        /// Enum describing device type
        /// </summary>
        public CorsairDeviceType type;

        /// <summary>
        /// Null-terminated device model (like “K95RGB”)
        /// </summary>
        public IntPtr model;

        /// <summary>
        /// Enum describing physical layout of the keyboard or mouse. If device is neither keyboard nor mouse then value is CPL_Invalid
        /// </summary>
        public CorsairPhysicalLayout physicalLayout;

        /// <summary>
        /// Enum describing logical layout of the keyboard as set in CUE settings. If device is not keyboard then value is CLL_Invalid
        /// </summary>
        public CorsairLogicalLayout logicalLayout;

        /// <summary>
        /// Mask that describes device capabilities, formed as logical “or” of CorsairDeviceCaps enum values
        /// </summary>
        public int capsMask;

        /// <summary>
        /// Number of controllable LEDs on the device
        /// </summary>
        public int ledsCount;

        /// <summary>
        /// Structure that describes channels of the DIY-devices and coolers.
        /// </summary>
        public CorsairChannelsInfoNative channels;
    }
}
