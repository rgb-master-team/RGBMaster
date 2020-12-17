using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Common
{
    public class StaticColorEffectProps : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int relativeSmoothness;

        public Color SelectedColor { get; set; }
        public byte SelectedBrightness { get; set; }

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

    public class StaticColorEffectMetadata : EffectMetadata<StaticColorEffectProps>
    {
        public override string EffectName => "Color Picker Sync";

        public override string ShortDescription => "Syncs the chosen static color to all selected devices.";

        public override string FullDescription => ShortDescription;

        public override string IconGlyph => "\uEF3C";

        public override EffectType Type => EffectType.StaticColor;

        public StaticColorEffectMetadata()
        {
            UpdateProps(new StaticColorEffectProps() { RelativeSmoothness = 0, SelectedBrightness = 100, SelectedColor = Color.White });
        }
    }
}
