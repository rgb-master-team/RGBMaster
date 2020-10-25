using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class GradientEffectMetadata : EffectMetadata
    {
        public override string EffectName => "Gradient sync";

        public override string ShortDescription => "Sync the color of your lights based on the configured gradient pattern.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uEE40";

        public override EffectType Type => EffectType.Gradient;
    }
}
