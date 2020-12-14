using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MusicEffectMetadataProperties
    {
        public List<MusicEffectAudioPoint> AudioPoints { get; set; }
        public AudioCaptureDevice CaptureDevice { get; set; }
        public MusicEffectBrightnessMode BrightnessMode { get; set; }
    }
}
