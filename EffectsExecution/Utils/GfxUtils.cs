using ColorThiefDotNet;
using EffectsExecution.Win32Api;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using Color = System.Drawing.Color;

namespace EffectsExecution.Utils
{
    internal static class GfxUtils
    {
        private static readonly ColorThief colorThief = new ColorThief();

        internal static Point GetCursorLocation()
        {
            Point point = new Point();
            Win32ApiMethods.GetCursorPos(ref point);

            return point;
        }

        internal static Color GetColorAt(int x, int y)
        {
            var screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = Win32Api.Win32ApiMethods.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, x, y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        internal static Size GetHeightAndWidth()
        {
            DEVMODE devMode = default;
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            Win32Api.Win32ApiMethods.EnumDisplaySettings(null, Win32Api.Win32ApiMethods.ENUM_CURRENT_SETTINGS, ref devMode);

            var height = devMode.dmPelsHeight;
            var width = devMode.dmPelsWidth;

            return new Size(width, height);
        }

        internal static Color GetDominantColorFromThief()
        {
            var dimensions = GetHeightAndWidth();
            var height = dimensions.Height;
            var width = dimensions.Width;

            var captureBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            /*using var captureGraphic = Graphics.FromImage(captureBmp);
            //captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size, CopyPixelOperation.SourceCopy);*/

            using (Graphics gdest = Graphics.FromImage(captureBmp))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();

                    int retval = Win32ApiMethods.BitBlt(hDC, 0, 0, width, height, hSrcDC, 0, 0, (int)CopyPixelOperation.SourceCopy);

                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            // TODO - Support letting the user change the desired resolution to which the screen
            // output is downsampled to.
            // Also - ensure we support all aspect ratios.
            var downSampledBitmap = new Bitmap(captureBmp, new Size(800, 600));

            var thiefColor = colorThief.GetColor(downSampledBitmap, 100, false);

            return Color.FromArgb(thiefColor.Color.R, thiefColor.Color.G, thiefColor.Color.B);
        }
    }
}
