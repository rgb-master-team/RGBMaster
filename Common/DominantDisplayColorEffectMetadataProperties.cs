using System.ComponentModel;
using Utils;

namespace Common
{
    public class DominantDisplayColorEffectMetadataProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int relativeSmoothness;
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