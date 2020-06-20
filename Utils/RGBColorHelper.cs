using System;
using System.Drawing;

namespace Utils
{
    /// <summary>
    /// Helper for colors
    /// </summary>
    public static class RGBColorHelper
    {
        #region Internal Methods

        /// <summary>
        /// Compute a RGB color
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ComputeRGBColor(int r, int g, int b)
        {
            return r << 16 | g << 8 | b;
        }

        public static Color ParseColor(int computedColor)
        {
            int r = (byte)(computedColor >> 16); // = 0
            int g = (byte)(computedColor >> 8); // = 0
            int b = (byte)(computedColor >> 0); // = 255
            return Color.FromArgb(r, g, b);
        }

        #endregion Internal Methods
    }
}