using Common;
using System.Drawing;

namespace MagicHome
{
    public class MagicHomeProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "MagicHome";

        public override string ProviderShortDescription => "Magic Home devices provider.";

        public override string ProviderFullDescription => "Magic Home devices provider, that provides devices that are connected to the platform, usually via controllers or app integration.";

        public override string ProviderIconAssetPath => @"/Assets/Logos/MagicHomeLogo.png";
        public override string ProviderUrl => null;
    }
}