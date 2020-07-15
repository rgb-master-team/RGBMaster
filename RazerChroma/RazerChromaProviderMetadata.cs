using Common;
using System.Drawing;

namespace RazerChroma
{
    public class RazerChromaProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Razer Chroma";

        public override string ProviderShortDescription => "Razer Chroma devices provider.";

        public override string ProviderFullDescription => "Razer Chroma devices provider, providing every Razer device that supports Chroma or any other non-Razer device that integrates with Razer Chroma technology.";

        public override string ProviderIconAssetPath => "/Assets/Logos/SynapseLogo.png";
    }
}