using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicHome
{
    public class MagicHomeProvider : BaseProvider
    {
        public MagicHomeProvider(): base(new MagicHomeProviderMetadata())
        {

        }

        public override Task<List<Device>> Discover()
        {
            List<Light> internalDevices;
            internalDevices = Light.Discover();

            return Task.FromResult(internalDevices.Select(internalDevice => new MagicHomeDevice(internalDevice, new MagicHomeDeviceMetadata(ProviderMetadata.ProviderGuid, "Magic Home Device"))).ToList<Device>());
        }

        protected override Task InternalRegister()
        {
            return Task.CompletedTask;
        }

        protected override Task InternalUnregister()
        {
            return Task.CompletedTask;
        }
    }
}
