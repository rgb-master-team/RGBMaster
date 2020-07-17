using Common;
using System.Drawing;

namespace Aura
{
    public class AuraProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Aura Sync";

        public override string ProviderShortDescription => "Aura Sync provider for all Asus Aura devices.";

        public override string ProviderFullDescription => "Asus Aura sync provider for all Asus Aura devices or devices that are compatible with Asus Aura.";

        public override string ProviderIconAssetPath => null;

        public override string ProviderUrl => "http://www.google.com";
    }
}