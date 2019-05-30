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

        public override IEnumerable<OperationType> SupportedOperations => auraSupportedOps;

        public override string ProviderName => "Aura Sync";

        public override Task<IEnumerable<Device>> Discover()
        {
            var sdk = new AuraSDK("Lib/AURA_SDK.dll");

            return Task.FromResult<IEnumerable<Device>>(sdk.Motherboards.Select(mb => new AuraDevice(mb)));
        }
    }
}
