using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common;
using Corsair.Device;
using Corsair.Enums;
using Corsair.Led;
using Corsair.Protocol;
using Corsair.Provider;

namespace Corsair.CUESDK
{
    /// <summary>
    /// The Corsair Utility Engine (CUE) SDK gives ability for third-party applications to control lightings on Corsair RGB devices. CUE SDK interacts with hardware through CUE so it should be running in order for SDK to work properly.
    /// </summary>
    public class CUESDK
    {
        private static DeviceType GetDeviceTypeForCorsair(Corsair.Device.CorsairDeviceType internalDevice)
        {
            switch (internalDevice)
            {
                case CorsairDeviceType.Unknown:
                    return DeviceType.Unknown;
                case CorsairDeviceType.Mouse:
                    return DeviceType.Mouse;
                case CorsairDeviceType.Keyboard:
                    return DeviceType.Keyboard;
                case CorsairDeviceType.Headset:
                    return DeviceType.Headset;
                case CorsairDeviceType.MouseMat:
                    return DeviceType.Mousepad;
                case CorsairDeviceType.HeadsetStand:
                    return DeviceType.Unknown;
                case CorsairDeviceType.CommanderPro:
                    return DeviceType.Fan;
                case CorsairDeviceType.LightingNodePro:
                    return DeviceType.LedStrip;
                case CorsairDeviceType.MemoryModule:
                    return DeviceType.Memory;
                case CorsairDeviceType.Cooler:
                    return DeviceType.Fan;
                default:
                    return DeviceType.Unknown;
            }
        }

        /// <summary>
        /// Get all the connected device from the cue driver
        /// </summary>
        /// <returns>List of all the connected devices</returns>
        public static List<CorsairDevice> GetAllDevices(Guid discoveringProvider)
	    {
		    var devicesCount = GetDeviceCount();

		    var devices = new List<CorsairDevice>();
		    for (var deviceIndex = 0; deviceIndex < devicesCount; deviceIndex++)
		    {
			    devices.Add(GetDeviceInfo(deviceIndex, discoveringProvider));

			    devices[deviceIndex].Load();
			}

		    return devices;
	    }

        /// <summary>
        /// Set specified leds to some colors. The color is retained until changed by successive calls. This function does not take logical layout into account. This function executes synchronously, if you are concerned about delays consider using SetLedsColorsAsync
        /// </summary>
        /// <param name="ledsCount">Number of leds in ledsColors array</param>
        /// <param name="ledsColors">Array containing colors for each LED.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then function completes successfully and returns true.</returns>
        [Obsolete("It is not recommended to use this function with DIY-devices, coolers and memory modules. Consider using SetLedsColorsBufferByDeviceIndex() to fill buffer and SetLedsColorsFlushBuffer() to send data to CUE instead.")]
        public static bool SetLedsColors(int ledsCount, CorsairLedColor[] ledsColors)
        {
            var corsairLedColorSize = Marshal.SizeOf<CorsairLedColorNative>();
            var ledsPtr = Marshal.AllocHGlobal(corsairLedColorSize * ledsCount);

            for (var ledIndex = 0; ledIndex < ledsCount; ledIndex++)
            {
                ledsColors[ledIndex].ApplyToNative();

                Marshal.StructureToPtr(ledsColors[ledIndex].Native, ledsPtr + corsairLedColorSize * ledIndex, false);
            }

            var result = CUESDKNative.CorsairSetLedsColors(ledsCount, ledsPtr);

            Marshal.FreeHGlobal(ledsPtr);

            return result;
        }

        /// <summary>
        /// Set specified LEDs to some colors. This function set LEDs colors in the buffer which is written to the devices via SetLedsColorsFlushBuffer or SetLedsColorsFlushBufferAsync. Typical usecase is next: SetLedsColorsFlushBuffer or SetLedsColorsFlushBufferAsync is called to write LEDs colors to the device and follows after one or more calls of SetLedsColorsBufferByDeviceIndex to set the LEDs buffer. This function does not take logical layout into account.
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by GetDeviceCount()</param>
        /// <param name="ledsCount">Number of leds in ledsColors array</param>
        /// <param name="ledsColors">Array containing colors for each LED.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
        public static bool SetLedsColorsBufferByDeviceIndex(int deviceIndex, int ledsCount, CorsairLedColor[] ledsColors)
        {
            var corsairLedColorSize = Marshal.SizeOf<CorsairLedColorNative>();
            var ledsPtr = Marshal.AllocHGlobal(corsairLedColorSize * ledsCount);

            for (var ledIndex = 0; ledIndex < ledsCount; ledIndex++)
            {
                ledsColors[ledIndex].ApplyToNative();

                Marshal.StructureToPtr(ledsColors[ledIndex].Native, ledsPtr + corsairLedColorSize * ledIndex, false);
            }

            var result = CUESDKNative.CorsairSetLedsColorsBufferByDeviceIndex(deviceIndex, ledsCount, ledsPtr);

            Marshal.FreeHGlobal(ledsPtr);

            return result;
        }

        /// <summary>
        /// Writes to the devices LEDs colors buffer which is previously filled by the SetLedsColorsBufferByDeviceIndex function. This function executes synchronously, if you are concerned about delays consider using SetLedsColorsFlushBufferAsync
        /// </summary>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId in the LEDs colors buffer present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
        public static bool SetLedsColorsFlushBuffer()
        {
            return CUESDKNative.CorsairSetLedsColorsFlushBuffer();
        }

        /// <summary>
        /// Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result
        /// </summary>
        /// <param name="context">Context contains value that was supplied by user in SetLedsColorsFlushBufferAsync call.</param>
        /// <param name="result">Result is true if call was successful, otherwise false</param>
        /// <param name="error">Error contains error code if call was not successful (result==false)</param>
        public delegate void SetLedsColorsFlushBufferAsyncCallback(object context, bool result, CorsairError error);

        /// <summary>
        /// Same as SetLedsColorsFlushBuffer but returns control to the caller immediately.
        /// </summary>
        /// <param name="callback">Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result</param>
        /// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId in the LEDs colors buffer present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
        public static bool SetLedsColorsFlushBufferAsync(SetLedsColorsFlushBufferAsyncCallback callback, object context)
        {
            var callbackMethod = new CUESDKNative.CorsairSetLedsColorsFlushBufferAsyncCallback((IntPtr nativeContext, bool result, CorsairError error) =>
            {
                callback?.Invoke(context, result, error);
            });

            return CUESDKNative.CorsairSetLedsColorsFlushBufferAsync(callbackMethod, new IntPtr());
        }

        /// <summary>
        /// Get current color for the list of requested LEDs. The color should represent the actual state of the hardware LED, which could be a combination of SDK and/or CUE input. This function works only for keyboard, mouse, mousemat, headset and headset stand devices.
        /// </summary>
        /// <param name="size">Number of leds in ledsColors array</param>
        /// <param name="ledsColors">Array containing colors for each LED. Caller should only fill ledId field, and then SDK will fill R, G and B values on return</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true. Also ledsColors array will contain R, G and B values of colors on return.</returns>
        public static bool GetLedsColors(int size, CorsairLedColor[] ledsColors)
        {
            var corsairLedColorSize = Marshal.SizeOf<CorsairLedColorNative>();
            var ledsPtr = Marshal.AllocHGlobal(corsairLedColorSize * size);

            for (var ledIndex = 0; ledIndex < size; ledIndex++)
            {
                ledsColors[ledIndex].ApplyToNative();

                Marshal.StructureToPtr(ledsColors[ledIndex].Native, ledsPtr + corsairLedColorSize * ledIndex, false);
            }

            var result = CUESDKNative.CorsairGetLedsColors(size, ledsPtr);

            for (var ledIndex = 0; ledIndex < size; ledIndex++)
            {
                ledsColors[ledIndex].Native = Marshal.PtrToStructure<CorsairLedColorNative>(ledsPtr + corsairLedColorSize * ledIndex);

                ledsColors[ledIndex].ApplyToManaged();
            }

            Marshal.FreeHGlobal(ledsPtr);

            return result;
        }

        /// <summary>
        /// Get current color for the list of requested LEDs. The color should represent the actual state of the hardware LED, which could be a combination of SDK and/or CUE input. This function works for keyboard, mouse, mousemat, headset, headset stand, DIY-devices, memory module and cooler.
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by GetDeviceCount()</param>
        /// <param name="ledsCount">Number of LEDs in ledsColors array</param>
        /// <param name="ledsColors">Array containing colors for each LED. Caller should only fill ledId field, and then SDK will fill R, G and B values on return.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true. Also ledsColors array will contain R, G and B values of colors on return.</returns>
        public static bool GetLedsColorsByDeviceIndex(int deviceIndex, int ledsCount, CorsairLedColor[] ledsColors)
        {
            var corsairLedColorSize = Marshal.SizeOf<CorsairLedColorNative>();
            var ledsPtr = Marshal.AllocHGlobal(corsairLedColorSize * ledsCount);

            for (var ledIndex = 0; ledIndex < ledsCount; ledIndex++)
            {
                ledsColors[ledIndex].ApplyToNative();
                Marshal.StructureToPtr(ledsColors[ledIndex].Native, ledsPtr + corsairLedColorSize * ledIndex, false);
            }

            var result = CUESDKNative.CorsairGetLedsColorsByDeviceIndex(deviceIndex, ledsCount, ledsPtr);

            for (var ledIndex = 0; ledIndex < ledsCount; ledIndex++)
            {
                ledsColors[ledIndex].Native = Marshal.PtrToStructure<CorsairLedColorNative>(ledsPtr + corsairLedColorSize * ledIndex);
                ledsColors[ledIndex].ApplyToManaged();
            }

            Marshal.FreeHGlobal(ledsPtr);

            return result;
        }

        /// <summary>
        /// Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result
        /// </summary>
        /// <param name="context">Context contains value that was supplied by user in SetLedsColorsAsync call</param>
        /// <param name="result">Result is true if call was successful, otherwise false</param>
        /// <param name="error">Error contains error code if call was not successful (result==false)</param>
        public delegate void SetLedsColorsAsyncCallback(object context, bool result, CorsairError error);

        /// <summary>
        /// Same as SetLedsColors but returns control to the caller immediately.
        /// </summary>
        /// <param name="ledsCount">Number of leds in ledsColors array</param>
        /// <param name="ledsColors">Array containing colors for each LED</param>
        /// <param name="callbackType">Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result</param>
        /// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure.</returns>
        [Obsolete("It is not recommended to use this function with DIY-devices, coolers and memory modules. Consider using SetLedsColorsBufferByDeviceIndex() to fill buffer and SetLedsColorsFlushBufferAsync() to send data to CUE instead.")]
        public static bool SetLedsColorsAsync(int ledsCount, CorsairLedColor[] ledsColors, SetLedsColorsAsyncCallback callbackType, object context)
        {
            var callbackMethod = new CUESDKNative.CorsairSetLedsColorsAsyncCallback((nativeContext, result, error) =>
            {
                callbackType?.Invoke(context, result, error);
            });

            var corsairLedColorSize = Marshal.SizeOf<CorsairLedColorNative>();
            var ledsPtr = Marshal.AllocHGlobal(corsairLedColorSize * ledsCount);

            for (var ledIndex = 0; ledIndex < ledsCount; ledIndex++)
            {
                ledsColors[ledIndex].ApplyToNative();

                Marshal.StructureToPtr(ledsColors[ledIndex].Native, ledsPtr + corsairLedColorSize * ledIndex, false);
            }

            var callResult = CUESDKNative.CorsairSetLedsColorsAsync(ledsCount, ledsPtr, callbackMethod, new IntPtr());

            Marshal.FreeHGlobal(ledsPtr);

            return callResult;
        }

        /// <summary>
        /// Returns number of connected Corsair devices. For keyboards, mice, mousemats, headsets and headset stands  not more than one device of each type is included in return value in case if there are multiple devices of same type connected to the system. For DIY-devices and coolers actual number of connected devices is included in return value. For memory modules actual number of connected modules is included in return value, modules are enumerated with respect to their logical position (counting from left to right, from top to bottom). Use GetDeviceInfo() to get information about a certain device.
        /// </summary>
        /// <returns>Integer value. -1 in case of error.</returns>
        public static int GetDeviceCount()
        {
            return CUESDKNative.CorsairGetDeviceCount();
        }

        /// <summary>
        /// Returns information about a device based on provided index.
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than a value returned by GetDeviceInfo()</param>
        /// <returns>CorsairDevice structure that contains information about device or NULL if error has occurred.</returns>
        public static CorsairDevice GetDeviceInfo(int deviceIndex, Guid discoveringProvider)
        {
            var deviceInfoPtr = CUESDKNative.CorsairGetDeviceInfo(deviceIndex);
            var deviceInfo = Marshal.PtrToStructure<CorsairDeviceNative>(deviceInfoPtr);

            return new CorsairDevice(deviceInfo, deviceIndex, new CorsairDeviceMetadata(discoveringProvider, GetDeviceTypeForCorsair(deviceInfo.type), deviceInfo.model != null ? Marshal.PtrToStringAuto(deviceInfo.model) : "Unknown", new HashSet<OperationType>() { OperationType.SetColor }));
        }

        /// <summary>
        /// Provides list of keyboard LEDs with their physical positions. Coordinates grids for different device models can be found in Device coordinates.
        /// </summary>
        /// <returns>Returns CorsairLedPositions struct or NULL if error has occured.</returns>
        public static CorsairLedPositions GetLedPositions()
        {
            var ledPositionsPtr = CUESDKNative.CorsairGetLedPositions();
            var ledPositions = Marshal.PtrToStructure<CorsairLedPositionsNative>(ledPositionsPtr);

            return new CorsairLedPositions(ledPositions);
        }

        /// <summary>
        /// Provides list of keyboard, mouse, headset, mousemat, headset stand, DIY-devices, memory module and cooler LEDs by its index with their positions. Position could be either physical (only device-dependent) or logical (depend on device as well as CUE settings).
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than a value returned by GetDeviceCount()</param>
        /// <returns>Returns CorsairLedPositions struct or NULL if error has occurred.</returns>
        public static CorsairLedPositions GetLedPositionsByDeviceIndex(int deviceIndex)
        {
            var ledPositionsPtr = CUESDKNative.CorsairGetLedPositionsByDeviceIndex(deviceIndex);
            var ledPositions = Marshal.PtrToStructure<CorsairLedPositionsNative>(ledPositionsPtr);

            return new CorsairLedPositions(ledPositions);
        }

        /// <summary>
        /// Retrieves led id for key name taking logical layout into account. So on AZERTY keyboards if user calls GetLedIdForKeyName(‘A’) he gets CLK_Q. This id can be used in SetLedsColors function.
        /// </summary>
        /// <param name="keyName">Key name. [‘A’..’Z’] (26 values) are valid values.</param>
        /// <returns>Proper CorsairLedId or CorserLed_Invalid if error occurred.</returns>
        public static CorsairLedId GetLedIdForKeyName(char keyName)
        {
            return CUESDKNative.CorsairGetLedIdForKeyName(keyName);
        }

        /// <summary>
        /// Requests control using specified access mode.  By default client has shared control over lighting so there is no need to call RequestControl() unless a client requires exclusive control.
        /// </summary>
        /// <param name="accessMode">Requested accessMode</param>
        /// <returns>Boolean value. Returns true if SDK received requested control or false otherwise.</returns>
        public static bool RequestControl(CorsairAccessMode accessMode)
        {
            return CUESDKNative.CorsairRequestControl(accessMode);
        }

        /// <summary>
        /// Checks file and protocol version of CUE to understand which of SDK functions can be used with this version of CUE.
        /// </summary>
        /// <returns>CorsairProtocolDetails struct.</returns>
        public static CorsairProtocolDetails PerformProtocolHandshake()
        {
            var protocolDetails = CUESDKNative.CorsairPerformProtocolHandshake();

            return new CorsairProtocolDetails(protocolDetails);
        }

        /// <summary>
        /// Returns last error that occurred in this thread while using any of Corsair* functions.
        /// </summary>
        /// <returns>CorsairError value.</returns>
        public static CorsairError GetLastError()
        {
            return CUESDKNative.CorsairGetLastError();
        }

        /// <summary>
        /// Releases previously requested control for specified access mode.
        /// </summary>
        /// <param name="accessMode">AccessMode that is requested to be released.</param>
        /// <returns>Boolean value. Returns true if SDK released control or false otherwise.</returns>
        public static bool ReleaseControl(CorsairAccessMode accessMode)
        {
            return CUESDKNative.CorsairReleaseControl(accessMode);
        }

        /// <summary>
        /// Set layer priority for this shared client. By default CUE has priority of 127 and all shared clients have priority of 128 if they don’t call this function. Layers with higher priority value are shown on top of layers with lower priority.
        /// </summary>
        /// <param name="priority">Priority of a layer [0..255]</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure. If this function is called in exclusive  mode then it will return true.</returns>
        public static bool SetLayerPriority(int priority)
        {
            return CUESDKNative.CorsairSetLayerPriority(priority);
        }

        /// <summary>
        /// Callback that is called by SDK when key is pressed or released
        /// </summary>
        /// <param name="context">Contains value that was supplied by user in RegisterKeypressCallback call.</param>
        /// <param name="keyId">The id of the key that was pressed or released</param>
        /// <param name="pressed">True if the key was pressed and false if it was released</param>
        public delegate void RegisterKeypressCallbackCallback(object context, CorsairKeyId keyId, bool pressed);

        /// <summary>
        /// Registers a callback that will be called by SDK when some of G or M keys are pressed or released
        /// </summary>
        /// <param name="CallbackType">Callback that is called by SDK when key is pressed or released</param>
        /// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure</returns>
        public static bool RegisterKeypressCallback(RegisterKeypressCallbackCallback CallbackType, object context)
        {
            var callbackMethod = new CUESDKNative.CorsairRegisterKeypressCallbackCallback((nativeContext, keyId, pressed) =>
            {
                CallbackType?.Invoke(context, keyId, pressed);
            });

            return CUESDKNative.CorsairRegisterKeypressCallback(callbackMethod, new IntPtr());
        }

        /// <summary>
        /// Reads boolean property value for device at provided index.
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by GetDeviceCount()</param>
        /// <param name="propertyId">Id of property to read from device</param>
        /// <param name="propertyValue">Boolean property value read from device.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure.</returns>
        public static bool GetBoolPropertyValue(int deviceIndex, CorsairDevicePropertyId propertyId, ref bool propertyValue)
        {
            var propertySize = Marshal.SizeOf<int>();
            var propertyPtr = Marshal.AllocHGlobal(propertySize);

            Marshal.WriteInt32(propertyPtr, Convert.ToInt32(propertyValue));

            var result = CUESDKNative.CorsairGetBoolPropertyValue(deviceIndex, propertyId, propertyPtr);

            propertyValue = Convert.ToBoolean(Marshal.ReadInt32(propertyPtr));

            Marshal.FreeHGlobal(propertyPtr);

            return result;
        }

        /// <summary>
        /// Reads integer property value for device at provided index.
        /// </summary>
        /// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by GetDeviceCount()</param>
        /// <param name="propertyId">Id of property to read from device</param>
        /// <param name="propertyValue">Integer property value read from device.</param>
        /// <returns>Boolean value. True if successful. Use GetLastError() to check the reason of failure.</returns>
        public static bool GetInt32PropertyValue(int deviceIndex, CorsairDevicePropertyId propertyId, ref int propertyValue)
        {
            var propertySize = Marshal.SizeOf<int>();
            var propertyPtr = Marshal.AllocHGlobal(propertySize);

            Marshal.WriteInt32(propertyPtr, propertyValue);

            var result = CUESDKNative.CorsairGetInt32PropertyValue(deviceIndex, propertyId, propertyPtr);

            propertyValue = Marshal.ReadInt32(propertyPtr);

            Marshal.FreeHGlobal(propertyPtr);

            return result;
        }
    }
}
