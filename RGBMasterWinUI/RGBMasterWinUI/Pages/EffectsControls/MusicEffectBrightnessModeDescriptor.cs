using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBMasterWinUI.Pages.EffectsControls
{
    // TODO - Move this class to another location, restructure effects controls.
    public class MusicEffectBrightnessModeDescriptor
    {
        public MusicEffectBrightnessMode Mode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GlyphIcon
        {
            get
            {
                switch (Mode)
                {
                    case MusicEffectBrightnessMode.Unchanged:
                        return "\uF140";
                    case MusicEffectBrightnessMode.ByHSL:
                        return "\uE790";
                    case MusicEffectBrightnessMode.ByVolumeLvl:
                        return "\uE993";
                    default:
                        break;
                }
                throw new NotImplementedException();
            }
        }
    }
}
