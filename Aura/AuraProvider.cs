using AuraSDKDotNet;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura
{
    public class AuraProvider : Provider
    {
        private readonly List<OperationType> auraSupportedOps = new List<OperationType>() { OperationType.SetColor };
        private AuraSDK internalSdk;

        public override IEnumerable<OperationType> SupportedOperations => auraSupportedOps;

        public override string ProviderName => "Aura Sync";

        public override Task<IEnumerable<Device>> Discover()
        {
            var sdk = new AuraSDK(@"C:/Users/Dean/Projects/chroma-yeelight-music/Aura/Lib/AuraSDK/sdk/AURA_SDK.dll");

            return Task.FromResult<IEnumerable<Device>>(sdk.Motherboards.Select(mb => new AuraDevice(mb)));
        }

        public override Task Register()
        {
            internalSdk = new AuraSDK(@"C:/Users/Dean/Projects/chroma-yeelight-music/Aura/Lib/AuraSDK/sdk/AURA_SDK.dll");
            return Task.CompletedTask;
        }

        public override Task Unregister()
        {
            internalSdk.Unload();
            return Task.CompletedTask;
        }
    }
}
