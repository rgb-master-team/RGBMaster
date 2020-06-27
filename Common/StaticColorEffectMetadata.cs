using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StaticColorEffectProps
    {
        public Color SelectedColor { get; set; }
        public byte SelectedBrightness { get; set; }
    }

    public class StaticColorEffectMetadata : EffectMetadata<StaticColorEffectProps>
    {
        public override string EffectName => "Color Picker Sync";

        public override string ShortDescription => "Syncs the chosen static color to all selected devices.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uEF3C";

        public override EffectType Type => EffectType.StaticColor;
    }
}
