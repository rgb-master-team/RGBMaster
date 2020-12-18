using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Utils;

namespace Common
{
    public class MusicEffectMetadataProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private int relativeSmoothness;

        public List<MusicEffectAudioPoint> AudioPoints { get; set; }
        public AudioCaptureDevice CaptureDevice { get; set; }
        public MusicEffectBrightnessMode BrightnessMode { get; set; }

        public int RelativeSmoothness
        {
            get
            {
                return relativeSmoothness;
            }
            set
            {
                relativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }
    }
}
