using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common
{
    public class GradientEffectMetadata : EffectMetadata<GradientEffectMetadataProperties>
    {
        public override string EffectName => "Gradient sync";

        public override string ShortDescription => "Sync the color of your lights based on the configured gradient pattern.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uEE40";

        public override EffectType Type => EffectType.Gradient;

        public GradientEffectMetadata()
        {
            EffectProperties = new GradientEffectMetadataProperties()
            {
                GradientPoints = new List<GradientPoint>()
                {
                    new GradientPoint()
                    {
                        Index = 0,
                        Color = Color.Red
                    },
                    new GradientPoint()
                    {
                        Index = 1,
                        Color = Color.Green
                    },
                    new GradientPoint()
                    {
                        Index = 2,
                        Color = Color.Blue
                    }
                },
                RelativeSmoothness = 0,
                DelayInterval = 0,
            };
        }
    }
}
