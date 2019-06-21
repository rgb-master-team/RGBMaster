using System;
using System.IO;
using System.Runtime.InteropServices;
using Msi.Exceptions;

namespace Msi.SDKs
{
	public static class MysticLightSDKNative
	{
		private const string DLL_NAME = "MysticLight.dll";

		private static IntPtr dllPointer;

		#region SDK Functions Delegates

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_InitializePointer();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetDeviceInfoPointer([MarshalAs(UnmanagedType.SafeArray)]out string[] pDevType,
													      [MarshalAs(UnmanagedType.SafeArray)]out string[] pLedCount);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedInfoPointer(IntPtr deviceType, int index, out IntPtr pName,
													   [MarshalAs(UnmanagedType.SafeArray)]out string[] pLedStyles);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedColorPointer(IntPtr deviceType, int index, out int r, out int g, out int b);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedStylePointer(IntPtr deviceType, int index, out IntPtr style);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedMaxBrightPointer(IntPtr deviceType, int index, out int maxLevel);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedBrightPointer(IntPtr deviceType, int index, out int currentLevel);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedMaxSpeedPointer(IntPtr deviceType, int index, out int maxSpeed);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetLedSpeedPointer(IntPtr deviceType, int index, out int currentSpeed);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_SetLedColorPointer(IntPtr deviceType, int index, int r, int g, int b);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_SetLedStylePointer(IntPtr deviceType, int index, IntPtr style);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_SetLedBrightPointer(IntPtr deviceType, int index, int level);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_SetLedSpeedPointer(IntPtr deviceType, int index, int speed);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int MLAPI_GetErrorMessagePointer(int errorCode, out IntPtr pDesc);

		#endregion

		#region SDK Functions Pointers

		private static MLAPI_InitializePointer _MLAPI_Initialize;
		private static MLAPI_GetDeviceInfoPointer _MLAPI_GetDeviceInfo;
		private static MLAPI_GetLedInfoPointer _MLAPI_GetLedInfo;
		private static MLAPI_GetLedColorPointer _MLAPI_GetLedColor;
		private static MLAPI_GetLedStylePointer _MLAPI_GetLedStyle;
		private static MLAPI_GetLedMaxBrightPointer _MLAPI_GetLedMaxBright;
		private static MLAPI_GetLedBrightPointer _MLAPI_GetLedBright;
		private static MLAPI_GetLedMaxSpeedPointer _MLAPI_GetLedMaxSpeed;
		private static MLAPI_GetLedSpeedPointer _MLAPI_GetLedSpeed;
		private static MLAPI_SetLedColorPointer _MLAPI_SetLedColor;
		private static MLAPI_SetLedStylePointer _MLAPI_SetLedStyle;
		private static MLAPI_SetLedBrightPointer _MLAPI_SetLedBright;
		private static MLAPI_SetLedSpeedPointer _MLAPI_SetLedSpeed;
		private static MLAPI_GetErrorMessagePointer _MLAPI_GetErrorMessage;

		#endregion

		static MysticLightSDKNative()
		{
			if (!File.Exists(DLL_NAME))
			{
				throw new MysticLightErrorException("The dll was not found in the running directory");
			}

			LoadDllMethods();
		}

		private static void LoadDllMethods()
		{
			// Fuck you MSI
			var dllPath = Path.GetFullPath(DLL_NAME);
			dllPointer = LoadLibrary(DLL_NAME);

			// AAAAAAAAAAAAAAAAAA
			var why193 = Marshal.GetLastWin32Error().ToString();
			if (why193 == "193")
			{
				throw new MysticLightErrorException("Fuck you MSI, Make the god damm dll also 64 bit you assholes");
			}

			_MLAPI_Initialize = LoadDelegate<MLAPI_InitializePointer>(nameof(MLAPI_Initialize));
			_MLAPI_GetDeviceInfo = LoadDelegate<MLAPI_GetDeviceInfoPointer>(nameof(MLAPI_GetDeviceInfo));
			_MLAPI_GetLedInfo = LoadDelegate<MLAPI_GetLedInfoPointer>(nameof(MLAPI_GetLedInfo));
			_MLAPI_GetLedColor = LoadDelegate<MLAPI_GetLedColorPointer>(nameof(MLAPI_GetLedColor));
			_MLAPI_GetLedStyle = LoadDelegate<MLAPI_GetLedStylePointer>(nameof(MLAPI_GetLedStyle));
			_MLAPI_GetLedMaxBright = LoadDelegate<MLAPI_GetLedMaxBrightPointer>(nameof(MLAPI_GetLedMaxBright));
			_MLAPI_GetLedBright = LoadDelegate<MLAPI_GetLedBrightPointer>(nameof(MLAPI_GetLedBright));
			_MLAPI_GetLedMaxSpeed = LoadDelegate<MLAPI_GetLedMaxSpeedPointer>(nameof(MLAPI_GetLedMaxSpeed));
			_MLAPI_GetLedSpeed = LoadDelegate<MLAPI_GetLedSpeedPointer>(nameof(MLAPI_GetLedSpeed));
			_MLAPI_SetLedColor = LoadDelegate<MLAPI_SetLedColorPointer>(nameof(MLAPI_SetLedColor));
			_MLAPI_SetLedStyle = LoadDelegate<MLAPI_SetLedStylePointer>(nameof(MLAPI_SetLedStyle));
			_MLAPI_SetLedBright = LoadDelegate<MLAPI_SetLedBrightPointer>(nameof(MLAPI_SetLedBright));
			_MLAPI_SetLedSpeed = LoadDelegate<MLAPI_SetLedSpeedPointer>(nameof(MLAPI_SetLedSpeed));
			_MLAPI_GetErrorMessage = LoadDelegate<MLAPI_GetErrorMessagePointer>(nameof(MLAPI_GetErrorMessage));
		}

		private static T LoadDelegate<T>(string methodName) where T : Delegate => 
			(T)Marshal.GetDelegateForFunctionPointer(GetProcAddress(dllPointer, methodName), typeof(T));

		#region Kernel32 Methods

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LoadLibrary(string dllToLoad);

		[DllImport("kernel32.dll")]
		private static extern bool FreeLibrary(IntPtr dllHandle);

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr dllHandle, string name);

		#endregion

		#region API Methods

		internal static int MLAPI_Initialize() => _MLAPI_Initialize();

		internal static int MLAPI_GetDeviceInfo(out string[] pDevType, out string[] pLedCount) => _MLAPI_GetDeviceInfo(out pDevType, out pLedCount);

		internal static int MLAPI_GetLedInfo(IntPtr deviceType, int index, out IntPtr pName,
											 [MarshalAs(UnmanagedType.SafeArray)]out string[] pLedStyles) => _MLAPI_GetLedInfo(deviceType, index, out pName, out pLedStyles);

		internal static int MLAPI_GetLedColor(IntPtr deviceType, int index, out int r, out int g, out int b) => _MLAPI_GetLedColor(deviceType, index, out r, out g, out b);

		internal static int MLAPI_GetLedStyle(IntPtr deviceType, int index, out IntPtr style) => _MLAPI_GetLedStyle(deviceType, index, out style);

		internal static int MLAPI_GetLedMaxBright(IntPtr deviceType, int index, out int maxLevel) => _MLAPI_GetLedMaxBright(deviceType, index, out maxLevel);

		internal static int MLAPI_GetLedBright(IntPtr deviceType, int index, out int currentLevel) => _MLAPI_GetLedBright(deviceType, index, out currentLevel);

		internal static int MLAPI_GetLedMaxSpeed(IntPtr deviceType, int index, out int maxSpeed) => _MLAPI_GetLedMaxSpeed(deviceType, index, out maxSpeed);

		internal static int MLAPI_GetLedSpeed(IntPtr deviceType, int index, out int currentSpeed) => _MLAPI_GetLedSpeed(deviceType, index, out currentSpeed);

		internal static int MLAPI_SetLedColor(IntPtr deviceType, int index, int r, int g, int b) => _MLAPI_SetLedColor(deviceType, index, r, g, b);

		internal static int MLAPI_SetLedStyle(IntPtr deviceType, int index, IntPtr style) => _MLAPI_SetLedStyle(deviceType, index, style);

		internal static int MLAPI_SetLedBright(IntPtr deviceType, int index, int level) => _MLAPI_SetLedBright(deviceType, index, level);

		internal static int MLAPI_SetLedSpeed(IntPtr deviceType, int index, int speed) => _MLAPI_SetLedSpeed(deviceType, index, speed);

		internal static int MLAPI_GetErrorMessage(int errorCode, out IntPtr pDesc) => _MLAPI_GetErrorMessage(errorCode, out pDesc);

		internal static bool UnloadSDK()
		{
			return FreeLibrary(dllPointer);
		}

		#endregion
	}
}
