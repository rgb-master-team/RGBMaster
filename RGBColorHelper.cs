using System;

namespace chroma_yeelight
{
    /// <summary>
    /// Helper for colors
    /// </summary>
    public static class ColorHelper
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

        #endregion Internal Methods
    }
}