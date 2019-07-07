using System;
using System.Drawing;

namespace Infrastructure
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
            return ((r) << 16) | ((g) << 8) | (b);
        }

        public static Color ParseColor(int computedColor)
        {
            int r = ((byte)(computedColor >> 16)); // = 0
            int g = ((byte)(computedColor >> 8)); // = 0
            int b = ((byte)(computedColor >> 0)); // = 255
            return Color.FromArgb(r, g, b);
        }

        public static Color HSLToRGBColor(double h, double l, double s)
        {
	        double p2;
	        if (l <= 0.5)
	        {
		        p2 = l * (1 + s);
	        }
	        else
	        {
		        p2 = l + s - l * s;
	        }

	        var p1 = 2 * l - p2;
	        double r, g, b;

	        if (s == 0)
	        {
		        r = l;
		        g = l;
		        b = l;
	        }
	        else
	        {
		        r = QqhToRgb(p1, p2, h + 120);
		        g = QqhToRgb(p1, p2, h);
		        b = QqhToRgb(p1, p2, h - 120);
	        }

	        return Color.FromArgb((int)(r * 255.0), (int)(g * 255.0), (int)(b * 255.0));
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
	        if (hue > 360)
	        {
		        hue -= 360;
	        }
	        else if (hue < 0)
	        {
		        hue += 360;
	        }

	        if (hue < 60)
	        {
		        return q1 + (q2 - q1) * hue / 60;
	        }

	        if (hue < 180)
	        {
		        return q2;
	        }

	        if (hue < 240)
	        {
		        return q1 + (q2 - q1) * (240 - hue) / 60;
	        }

	        return q1;
        }

		#endregion Internal Methods
	}
}