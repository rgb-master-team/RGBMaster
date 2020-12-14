using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBMasterUWPApp.Pages.EffectsControls
{
    // TODO - Move this class to another location, restructure effects controls.
    public class MusicEffectBrightnessModeDescriptor
    {
        public MusicEffectBrightnessMode Mode { get; set; }
        public string Title { get; set; }
        public string GlyphIcon 
        { 
            get 
            {
                switch (Mode)
                {
                    case MusicEffectBrightnessMode.Unchanged:
                        break;
                    case MusicEffectBrightnessMode.ByHSL:
                        break;
                    case MusicEffectBrightnessMode.ByVolumeLvl:
                        break;
                    default:
                        break;
                }
                throw new NotImplementedException();
            } 
        }
    }
}
