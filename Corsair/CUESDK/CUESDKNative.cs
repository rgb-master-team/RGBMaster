using System;
using System.Runtime.InteropServices;
using Corsair.DeviceImpl;
using Corsair.Enums;
using Corsair.Led;
using Corsair.Protocol;

namespace Corsair.CUESDK
{
	/// <summary>
	/// The Corsair Utility Engine (CUE) SDK gives ability for third-party applications to control lightings on Corsair RGB devices. CUE SDK interacts with hardware through CUE so it should be running in order for SDK to work properly.
	/// </summary>
	public static class CUESDKNative
	{
		/// <summary>
		/// Set specified leds to some colors. The color is retained until changed by successive calls. This function does not take logical layout into account. This function executes synchronously, if you are concerned about delays consider using CorsairSetLedsColorsAsync
		/// </summary>
		/// <param name="size">Number of leds in ledsColors array</param>
		/// <param name="ledsColors">Array containing colors for each LED.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then function completes successfully and returns true.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLedsColors(int size, IntPtr ledsColors);

		/// <summary>
		/// Set specified LEDs to some colors. This function set LEDs colors in the buffer which is written to the devices via CorsairSetLedsColorsFlushBuffer or CorsairSetLedsColorsFlushBufferAsync. Typical usecase is next: CorsairSetLedsColorsFlushBuffer or CorsairSetLedsColorsFlushBufferAsync is called to write LEDs colors to the device and follows after one or more calls of CorsairSetLedsColorsBufferByDeviceIndex to set the LEDs buffer. This function does not take logical layout into account.
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by CorsairGetDeviceCount()</param>
		/// <param name="size">Number of leds in ledsColors array</param>
		/// <param name="ledsColors">Array containing colors for each LED.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLedsColorsBufferByDeviceIndex(int deviceIndex, int size, IntPtr ledsColors);

		/// <summary>
		/// Writes to the devices LEDs colors buffer which is previously filled by the CorsairSetLedsColorsBufferByDeviceIndex function. This function executes synchronously, if you are concerned about delays consider using CorsairSetLedsColorsFlushBufferAsync
		/// </summary>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId in the LEDs colors buffer present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLedsColorsFlushBuffer();

		/// <summary>
		/// Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result
		/// </summary>
		/// <param name="context">Context contains value that was supplied by user in CorsairSetLedsColorsFlushBufferAsync call.</param>
		/// <param name="result">Result is true if call was successful, otherwise false</param>
		/// <param name="error">Error contains error code if call was not successful (result==false)</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CorsairSetLedsColorsFlushBufferAsyncCallback(IntPtr context, bool result, CorsairError error);

		/// <summary>
		/// Same as CorsairSetLedsColorsFlushBuffer but returns control to the caller immediately.
		/// </summary>
		/// <param name="callback">Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result</param>
		/// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId in the LEDs colors buffer present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLedsColorsFlushBufferAsync(CorsairSetLedsColorsFlushBufferAsyncCallback callback, IntPtr context);

		/// <summary>
		/// Get current color for the list of requested LEDs. The color should represent the actual state of the hardware LED, which could be a combination of SDK and/or CUE input. This function works only for keyboard, mouse, mousemat, headset and headset stand devices.
		/// </summary>
		/// <param name="size">Number of leds in ledsColors array</param>
		/// <param name="ledsColors">Array containing colors for each LED. Caller should only fill ledId field, and then SDK will fill R, G and B values on return</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true. Also ledsColors array will contain R, G and B values of colors on return.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairGetLedsColors(int size, IntPtr ledsColors);

		/// <summary>
		/// Get current color for the list of requested LEDs. The color should represent the actual state of the hardware LED, which could be a combination of SDK and/or CUE input. This function works for keyboard, mouse, mousemat, headset, headset stand, DIY-devices, memory module and cooler.
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by CorsairGetDeviceCount()</param>
		/// <param name="size">Number of LEDs in ledsColors array</param>
		/// <param name="ledsColors">Array containing colors for each LED. Caller should only fill ledId field, and then SDK will fill R, G and B values on return.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If there is no such ledId present in currently connected hardware (missing key in physical keyboard layout, or trying to control mouse while it’s disconnected) then functions completes successfully and returns true. Also ledsColors array will contain R, G and B values of colors on return.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairGetLedsColorsByDeviceIndex(int deviceIndex, int size, IntPtr ledsColors);

		/// <summary>
		/// Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result
		/// </summary>
		/// <param name="context">Context contains value that was supplied by user in CorsairSetLedsColorsAsync call</param>
		/// <param name="result">Result is true if call was successful, otherwise false</param>
		/// <param name="error">Error contains error code if call was not successful (result==false)</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CorsairSetLedsColorsAsyncCallback(IntPtr context, bool result, CorsairError error);

		/// <summary>
		/// Same as CorsairSetLedsColors but returns control to the caller immediately.
		/// </summary>
		/// <param name="size">Number of leds in ledsColors array</param>
		/// <param name="ledsColors">Array containing colors for each LED</param>
		/// <param name="callbackType">Callback that is called by SDK when colors are set. Can be NULL if client is not interested in result</param>
		/// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLedsColorsAsync(int size, IntPtr ledsColors, CorsairSetLedsColorsAsyncCallback callbackType, IntPtr context);

		/// <summary>
		/// Returns number of connected Corsair devices. For keyboards, mice, mousemats, headsets and headset stands  not more than one device of each type is included in return value in case if there are multiple devices of same type connected to the system. For DIY-devices and coolers actual number of connected devices is included in return value. For memory modules actual number of connected modules is included in return value, modules are enumerated with respect to their logical position (counting from left to right, from top to bottom). Use CorsairGetDeviceInfo() to get information about a certain device.
		/// </summary>
		/// <returns>Integer value. -1 in case of error.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int CorsairGetDeviceCount();

		/// <summary>
		/// Returns information about a device based on provided index.
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than a value returned by CorsairGetDeviceInfo()</param>
		/// <returns>Pointer to CorsairDevice structure that contains information about device or NULL pointer if error has occurred.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CorsairGetDeviceInfo(int deviceIndex);

		/// <summary>
		/// Provides list of keyboard LEDs with their physical positions. Coordinates grids for different device models can be found in Device coordinates.
		/// </summary>
		/// <returns>Returns pointer to CorsairLedPositions struct or NULL if error has occured.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CorsairGetLedPositions();

		/// <summary>
		/// Provides list of keyboard, mouse, headset, mousemat, headset stand, DIY-devices, memory module and cooler LEDs by its index with their positions. Position could be either physical (only device-dependent) or logical (depend on device as well as CUE settings).
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than a value returned by CorsairGetDeviceCount()</param>
		/// <returns>Returns pointer to CorsairLedPositions struct or NULL if error has occurred.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CorsairGetLedPositionsByDeviceIndex(int deviceIndex);

		/// <summary>
		/// Retrieves led id for key name taking logical layout into account. So on AZERTY keyboards if user calls CorsairGetLedIdForKeyName(‘A’) he gets CLK_Q. This id can be used in CorsairSetLedsColors function.
		/// </summary>
		/// <param name="keyName">Key name. [‘A’..’Z’] (26 values) are valid values.</param>
		/// <returns>Proper CorsairLedId or CorserLed_Invalid if error occurred.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern CorsairLedId CorsairGetLedIdForKeyName(char keyName);

		/// <summary>
		/// Requests control using specified access mode.  By default client has shared control over lighting so there is no need to call CorsairRequestControl() unless a client requires exclusive control.
		/// </summary>
		/// <param name="accessMode">Requested accessMode</param>
		/// <returns>Boolean value. Returns true if SDK received requested control or false otherwise.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairRequestControl(CorsairAccessMode accessMode);

		/// <summary>
		/// Checks file and protocol version of CUE to understand which of SDK functions can be used with this version of CUE.
		/// </summary>
		/// <returns>CorsairProtocolDetails struct.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern CorsairProtocolDetailsNative CorsairPerformProtocolHandshake();

		/// <summary>
		/// Returns last error that occurred in this thread while using any of Corsair* functions.
		/// </summary>
		/// <returns>CorsairError value.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern CorsairError CorsairGetLastError();

		/// <summary>
		/// Releases previously requested control for specified access mode.
		/// </summary>
		/// <param name="accessMode">AccessMode that is requested to be released.</param>
		/// <returns>Boolean value. Returns true if SDK released control or false otherwise.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairReleaseControl(CorsairAccessMode accessMode);

		/// <summary>
		/// Set layer priority for this shared client. By default CUE has priority of 127 and all shared clients have priority of 128 if they don’t call this function. Layers with higher priority value are shown on top of layers with lower priority.
		/// </summary>
		/// <param name="priority">Priority of a layer [0..255]</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure. If this function is called in exclusive  mode then it will return true.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairSetLayerPriority(int priority);

		/// <summary>
		/// Callback that is called by SDK when key is pressed or released
		/// </summary>
		/// <param name="context">Contains value that was supplied by user in CorsairRegisterKeypressCallback call.</param>
		/// <param name="keyId">The id of the key that was pressed or released</param>
		/// <param name="pressed">True if the key was pressed and false if it was released</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CorsairRegisterKeypressCallbackCallback(IntPtr context, CorsairKeyId keyId, bool pressed);

		/// <summary>
		/// Registers a callback that will be called by SDK when some of G or M keys are pressed or released
		/// </summary>
		/// <param name="CallbackType">Callback that is called by SDK when key is pressed or released</param>
		/// <param name="context">Arbitrary context that will be returned in callback call. Can be NULL.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairRegisterKeypressCallback(CorsairRegisterKeypressCallbackCallback CallbackType, IntPtr context);

		/// <summary>
		/// Reads boolean property value for device at provided index.
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by CorsairGetDeviceCount()</param>
		/// <param name="propertyId">Id of property to read from device</param>
		/// <param name="propertyValue">Pointer to memory where to store boolean property value read from device.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairGetBoolPropertyValue(int deviceIndex, CorsairDevicePropertyId propertyId, IntPtr propertyValue);

		/// <summary>
		/// Reads integer property value for device at provided index.
		/// </summary>
		/// <param name="deviceIndex">Zero-based index of device. Should be strictly less than value returned by CorsairGetDeviceCount()</param>
		/// <param name="propertyId">Id of property to read from device</param>
		/// <param name="propertyValue">Pointer to memory where to store integer property value read from device.</param>
		/// <returns>Boolean value. True if successful. Use CorsairGetLastError() to check the reason of failure.</returns>
		[DllImport("CUESDK.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CorsairGetInt32PropertyValue(int deviceIndex, CorsairDevicePropertyId propertyId, IntPtr propertyValue);
	}
}
