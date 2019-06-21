using System;
using System.Runtime.InteropServices;

namespace Msi.SDKs
{
	public static class MysticLightSDKNative
	{
		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_Initialize();

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetDeviceInfo([MarshalAs(UnmanagedType.SafeArray)]out string[] pDevType,
													   [MarshalAs(UnmanagedType.SafeArray)]out string[] pLedCount);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedInfo(IntPtr deviceType, int index, out IntPtr pName,
													[MarshalAs(UnmanagedType.SafeArray)]out string[] pLedStyles);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedColor(IntPtr deviceType, int index, out int r, out int g, out int b);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedStyle(IntPtr deviceType, int index, out IntPtr style);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedMaxBright(IntPtr deviceType, int index, out int maxLevel);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedBright(IntPtr deviceType, int index, out int currentLevel);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedMaxSpeed(IntPtr deviceType, int index, out int maxSpeed);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetLedSpeed(IntPtr deviceType, int index, out int currentSpeed);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_SetLedColor(IntPtr deviceType, int index, int r, int g, int b);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_SetLedStyle(IntPtr deviceType, int index, IntPtr style);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_SetLedBright(IntPtr deviceType, int index, int level);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_SetLedSpeed(IntPtr deviceType, int index, int speed);

		[DllImport("MysticLight.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int MLAPI_GetErrorMessage(int errorCode, out IntPtr pDesc);
	}
}
