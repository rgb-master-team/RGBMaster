using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CursorColorEffectMetadata : EffectMetadata
    {
        public override EffectType Type => EffectType.CursorColor;

        public override string EffectName => "Cursor color";

        public override string ShortDescription => "Syncs the color by the mouse cursors location";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uE7C9";
    }
}
