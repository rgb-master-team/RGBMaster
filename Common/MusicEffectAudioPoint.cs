using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common
{
    public class MusicEffectAudioPoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double minimumAudioPoint;
        private Color color;

        public int Index { get; set; }
        public double MinimumAudioPoint 
        { 
            get
            {
                return minimumAudioPoint;
            }
            set
            {
                minimumAudioPoint = value;
                OnPropertyChanged();
            }
        }
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
