using AuraSDKDotNet;
using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraProvider : BaseProvider
    {
        private AuraSDK internalSdk;
        private static int VENDOR_ID = 0x1532;

        public AuraProvider() : base(new AuraProviderMetadata())
        {
        }

        public override Task<List<Device>> Discover()
        {
            return Task.FromResult(internalSdk.Motherboards.Select(mb => new AuraMotherboardDevice(mb, new AuraMotherboardDeviceMetadata(ProviderMetadata.ProviderGuid, "Unknown Aura Motherboard", new HashSet<OperationType>() { OperationType.SetColor }))).ToList<Device>());
        }

        protected override Task InternalRegister(CancellationToken cancellationToken = default)
        {
            internalSdk = new AuraSDK();
            return Task.CompletedTask;
        }

        protected override Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            internalSdk.Unload();
            return Task.CompletedTask;
        }
    }
}
