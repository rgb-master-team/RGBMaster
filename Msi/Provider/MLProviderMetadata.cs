using Common;
using System.Drawing;

namespace Msi.Provider
{
    public class MLProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "MSI Sync";

        public override string ProviderShortDescription => "MSI ML Provider.";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override string ProviderIconAssetPath => @"/Assets/Logos/MSIMysticLightLogo.png";
    }
}