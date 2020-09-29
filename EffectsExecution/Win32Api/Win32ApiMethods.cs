using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace EffectsExecution.Win32Api
{
    internal static class Win32ApiMethods
    {
        /* Win32 user */
        [DllImport("user32.dll")]
        internal static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("user32.dll")]
        internal static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        internal const int ENUM_CURRENT_SETTINGS = -1;


        /* GDI (Graphics library) */
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern uint GetPixel(IntPtr dc, int x, int y);
    }
}
