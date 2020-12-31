using Common;

namespace Corsair.ProviderImpl
{
    public class CorsairProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Corsair iCUE";

        public override string ProviderShortDescription => "Corsair Sync provider for all corsair synced devices.";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => @"/Assets/Logos/CorsairiCueLogo.png";
        public override string ProviderUrl => "https://www.corsair.com/ww/en/icue";
    }
}