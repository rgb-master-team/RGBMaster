using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using Utils;

namespace Common
{
    public class GradientPoint : INotifyPropertyChanged
    {
        private Color color;

        public event PropertyChangedEventHandler PropertyChanged;
        public int Index { get; set; }
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }
        public int RelativeSmoothness { get; set; }
        public int DelayInterval { get; set; }
    }
}