using Common;
using System.Drawing;

namespace Logitech
{
    public class LogitechProviderMetadata : ProviderMetadata
    {
        public override string ProviderName => "Logitech G Products Provider";

        public override string ProviderShortDescription => "Logitech G Products provider for all devices via the LG G Hub platform. Logitech devices only.";

        public override string ProviderFullDescription => ProviderShortDescription;

        public override Bitmap ProviderIcon => null;
    }
}