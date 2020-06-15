using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StaticColorEffectProps
    {
        public System.Drawing.Color SelectedColor { get; set; }
    }

    public class StaticColorEffectMetadata : EffectMetadata<StaticColorEffectProps>
    {
        public override string EffectName => "Static Color";

        public override string ShortDescription => "Syncs the chosen static color to all selected devices.";

        public override string FullDescription => ShortDescription;

        public override Bitmap Icon => null;
    }
}
