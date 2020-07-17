using Common;
using System.Drawing;

namespace Yeelight
{
    public class YeelightProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Xiaomi Yeelight";

        public override string ProviderShortDescription => "A provider for Xiaomi Yeelight devices.";

        public override string ProviderFullDescription => "A provider for Xiaomi Yeelight, providing bulbs and led strips.";

        public override string ProviderIconAssetPath => "/Assets/Logos/YeelightLogo.png";
        public override string ProviderUrl => "https://www.yeelight.com/";
    }
}