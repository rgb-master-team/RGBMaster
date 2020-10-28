using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Utils;

namespace Common
{
    public class GradientEffectMetadataProperties : INotifyPropertyChanged
    {
        private int relativeSmoothness;
        private int delayInterval;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<GradientPoint> GradientPoints { get; set; }

        public int RelativeSmoothness
        {
            get
            {
                return relativeSmoothness;
            }
            set
            {
                relativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(this.PropertyChanged, this);
            }
        }
        public int DelayInterval
        {
            get
            {
                return delayInterval;
            }
            set
            {
                delayInterval = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(this.PropertyChanged, this);
            }
        }
    }
}
