using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class DominantDisplayColorEffectMetadata : EffectMetadata
    {

        public override string EffectName => "Dominant display color sync";

        public override string ShortDescription => "Syncs lights with the most dominant color of a selected monitor.";

        public override string FullDescription => "Syncs lights with the most dominant color of a selected monitor. It continuously scans the pixels on your monitor like a video capture program, and with jumps of 8 pixels (so 7/8th the amount of pixels) the most common and dominant color is synced to all selected lights.";

        public override string IconGlyph => "\uE7F4";

        public override EffectType Type => EffectType.DominantColor;
    }
}
