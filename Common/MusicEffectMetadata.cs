using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class MusicEffectMetadata : EffectMetadata<MusicEffectMetadataProperties>
    {
        public override string EffectName => "Music Sync";

        public override string ShortDescription => "Syncs the color of your lights according to the max frequency of the played music/sound.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uF61F";

        public override EffectType Type => EffectType.Music;

        public MusicEffectMetadata()
        {
            EffectProperties = new MusicEffectMetadataProperties()
            {
                AudioPoints = new List<MusicEffectAudioPoint>() 
                { 
                    new MusicEffectAudioPoint()
                    {
                        Color = Color.White, 
                        Index = 0, 
                        MinimumAudioPoint = 0
                    }
                }
            };
        }
    }
}
