using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class MusicEffectMetadata : EffectMetadata
    {
        public override string EffectName => "Music";

        public override string ShortDescription => "Sync the color of the max frequency of the played music/sound.";

        public override string FullDescription => ShortDescription;

        public override Bitmap Icon => null;
    }
}
