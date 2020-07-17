using Common;
using System;
using System.Drawing;

namespace Logitech
{
    public class LogitechProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Logitech G";

        public override string ProviderShortDescription => "Logitech G Products provider for all devices via the LG G Hub platform. Logitech devices only.";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => @"/Assets/Logos/LogitechGHubLogo.png";

        public override string ProviderUrl => "https://www.logitechg.com/";
    }
}