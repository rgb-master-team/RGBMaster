using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class MusicEffectMetadata : EffectMetadata
    {
        public override string EffectName => "Music Sync";

        public override string ShortDescription => "Sync the color of the max frequency of the played music/sound.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uF61F";

        public override EffectType Type => EffectType.Music;
    }
}
