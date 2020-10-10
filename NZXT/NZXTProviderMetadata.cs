using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NZXT
{
    public class NZXTProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "NZXT";

        public override string ProviderShortDescription => "NZXT Provider";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => "/Assets/Logos/NZXTProviderLogo.png";

        public override string ProviderUrl => "https://www.nzxt.com/";
    }
}
