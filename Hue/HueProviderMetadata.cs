using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hue
{
    public class HueProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Phillips Hue";

        public override string ProviderShortDescription => "Phillips Hue";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => "/Assets/Logos/YeelightLogo.png";

        public override string ProviderUrl => "";
    }
}
