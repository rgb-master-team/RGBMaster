using AuraSDKDotNet;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraProvider : Provider<AuraProviderMetadata, AuraDeviceMetadata>
    {
        private AuraSDK internalSdk;

        public AuraProvider() : base(new AuraProviderMetadata())
        {
        }

        public override Task<IEnumerable<Device<AuraDeviceMetadata>>> Discover()
        {
            return Task.FromResult<IEnumerable<Device<AuraDeviceMetadata>>>(internalSdk.Motherboards.Select(mb => new AuraDevice(mb)));
        }

        protected override Task Register()
        {
            internalSdk = new AuraSDK();
            return Task.CompletedTask;
        }

        public override Task Unregister()
        {
            internalSdk.Unload();
            return Task.CompletedTask;
        }
    }
}
