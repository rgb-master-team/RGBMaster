using System.Drawing;

namespace Common
{
    public class GradientPoint
    {
        public int Index { get; set; }
        public Color Color { get; set; }
        public int RelativeSmoothness { get; set; }
        public int DelayInterval { get; set; }
    }
}