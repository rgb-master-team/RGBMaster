using AuraSDKDotNet;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraProvider : BaseProvider
    {
        private AuraSDK internalSdk;

        public AuraProvider() : base(new AuraProviderMetadata())
        {
        }

        public override Task<List<Device>> Discover()
        {
            return Task.FromResult(internalSdk.Motherboards.Select(mb => new AuraDevice(mb)).ToList<Device>());
        }

        protected override Task InternalRegister()
        {
            internalSdk = new AuraSDK();
            return Task.CompletedTask;
        }

        public override Task InternalUnregister()
        {
            internalSdk.Unload();
            return Task.CompletedTask;
        }
    }
}
