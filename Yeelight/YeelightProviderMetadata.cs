using Common;
using System.Drawing;

namespace Yeelight
{
    public class YeelightProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Xiaomi Yeelight";

        public override string ProviderShortDescription => "A provider for Xiaomi Yeelight devices.";

        public override string ProviderFullDescription => "A provider for Xiaomi Yeelight, providing bulbs and led strips.";

        public override Bitmap ProviderIcon => null;
    }
}