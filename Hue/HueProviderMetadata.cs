using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hue
{
    public class HueProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Phillips Hue";

        public override string ProviderShortDescription => "A provider for Phillips Hue devices.";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => "/Assets/Logos/HueLogo.png";

        public override string ProviderUrl => "https://www.philips-hue.com/";
    }
}
