using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Common;
using Msi.Devices;
using Msi.Enums;
using Msi.Exceptions;

namespace Msi.SDKs
{
	public static class MysticLightSdk
	{
		/// <summary>
		/// Returns all the devices loaded with their leds
		/// You must call Initialize before calling this method
		/// </summary>
		/// <returns>The list of all discovered devices</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static List<MLDevice> GetAllDevices(Guid providerGuid)
		{
			GetDeviceInfo(out var devicesTypes, out var ledsCount);

			var allDevices = devicesTypes.Select((deviceType, index) => new MLDevice(ledsCount[index], new Provider.MLDeviceMetadata(providerGuid, DeviceType.Unknown, deviceType))).ToList();

			return allDevices;
		}

		/// <summary>
		/// This function initializes the APIs
		/// </summary>
		/// <returns>The operation ML status code</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void Initialize()
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_Initialize());
		}

		/// <summary>
		/// Loads on the given parameters all the devices types and their leds count
		/// </summary>
		/// <param name="devicesType">Array of the devices types</param>
		/// <param name="ledsCount">Array of the amount of leds of each device</param>
		/// <returns>The operation ML status code</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void GetDeviceInfo(out string[] devicesType, out int[] ledsCount)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetDeviceInfo(out devicesType, out var ledCountStrings));

			ledsCount = ledCountStrings.Select(int.Parse).ToArray();
		}

		/// <summary>
		/// This function retrieves the information of the specified LED.
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <param name="ledName">The LED display name of the specified LED</param>
		/// <param name="ledStyles">The support styles of the specified LED</param>
		/// <returns>The operation ML status code</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void GetLedInfo(string deviceType, int ledIndex, out string ledName, out string[] ledStyles)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedInfo(Marshal.StringToBSTR(deviceType), ledIndex, out var ledNamePtr, out ledStyles));

			ledName = Marshal.PtrToStringBSTR(ledNamePtr);
		}

		/// <summary>
		/// Get the device's led color
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The color of the led of the device</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static Color GetLedColor(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedColor(Marshal.StringToBSTR(deviceType), ledIndex, 
																				 out var red, out var green, out var blue));

			return Color.FromArgb(0, red, green, blue);
		}

		/// <summary>
		/// Retrieves the specific LED current style.
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The led style name</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static string GetLedStyle(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes) MysticLightSDKNative.MLAPI_GetLedStyle(Marshal.StringToBSTR(deviceType),
																					ledIndex,
																					out var ledStyle));

			return Marshal.PtrToStringBSTR(ledStyle);
		}

		/// <summary>
		/// This function retrieves a specific LED supports the maximum brightness level.
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The led max bright</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static int GetLedMaxBright(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedMaxBright(Marshal.StringToBSTR(deviceType),
				ledIndex,
				out var ledMaxBright));

			return ledMaxBright;
		}

		/// <summary>
		///  Retrieves the specific LED current brightness level/
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The led bright level</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static int GetLedBright(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedBright(Marshal.StringToBSTR(deviceType), ledIndex,
				out var ledBright));

			return ledBright;
		}

		/// <summary>
		/// This function retrieves a specific LED supports the maximum speed level.
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The led max speed</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static int GetLedMaxSpeed(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedMaxSpeed(Marshal.StringToBSTR(deviceType), ledIndex,
				out var ledMaxSpeed));

			return ledMaxSpeed;
		}

		/// <summary>
		///  Retrieves the specific LED current speed level.
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <returns>The led speed level</returns>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static int GetLedSpeed(string deviceType, int ledIndex)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_GetLedSpeed(Marshal.StringToBSTR(deviceType), ledIndex,
				out var ledSpeed));

			return ledSpeed;
		}

		/// <summary>
		/// Set a specific led's color
		/// Be aware that not all styles support this method
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <param name="color">The new color</param>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void SetLedColor(string deviceType, int ledIndex, Color color)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_SetLedColor(Marshal.StringToBSTR(deviceType), ledIndex,
				color.R, color.G, color.B));
		}

		/// <summary>
		/// Set a specific led's style
		/// The style must be from one of the led's available styles
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <param name="style">The new style</param>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void SetLedStyle(string deviceType, int ledIndex, string style)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_SetLedStyle(Marshal.StringToBSTR(deviceType), ledIndex,
				Marshal.StringToBSTR(style)));
		}

		/// <summary>
		/// Set a specific led's bright level
		/// The bright level must be lower the led's max bright level
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <param name="brightLevel">The new bright level</param>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void SetLedBright(string deviceType, int ledIndex, int brightLevel)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_SetLedBright(Marshal.StringToBSTR(deviceType), ledIndex, brightLevel));
		}

		/// <summary>
		/// Set a specific led's speed level
		/// The speed level must be lower the led's max speed level
		/// </summary>
		/// <param name="deviceType">The type of the device</param>
		/// <param name="ledIndex">The LED identifier of the device</param>
		/// <param name="speedLevel">The new speed level</param>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		public static void SetLedSpeed(string deviceType, int ledIndex, int speedLevel)
		{
			EnsureStatusCode((MLStatusCodes)MysticLightSDKNative.MLAPI_SetLedSpeed(Marshal.StringToBSTR(deviceType), ledIndex, speedLevel));
		}

		/// <summary>
		/// This function converts a ML error code into general string.
		/// </summary>
		/// <param name="errorCode">ML error code</param>
		/// <returns>The error message</returns>
		internal static string GetErrorMessage(MLStatusCodes errorCode)
		{
			var statusCode = (MLStatusCodes)MysticLightSDKNative.MLAPI_GetErrorMessage((int)errorCode, out var errorPointer);

			return statusCode == MLStatusCodes.Ok ? Marshal.PtrToStringBSTR(errorPointer) : "The mystic light sdk could not return the error";
		}

		/// <summary>
		/// This method checks if the status is ok if not throws MysticLightErrorException
		/// with the Error Message from the mystic light sdk
		/// </summary>
		/// <param name="statusCode">The error code to check</param>
		/// <exception cref="MysticLightErrorException">The exception containing the error message from the sdk</exception>
		internal static void EnsureStatusCode(MLStatusCodes statusCode)
		{
			if (statusCode != MLStatusCodes.Ok)
			{
				throw new MysticLightErrorException(GetErrorMessage(statusCode));
			}
		}
	}
}
