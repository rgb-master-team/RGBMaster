using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;

namespace Yeelight
{
    public class YeelightProvider : BaseProvider
    {
        public YeelightProvider(): base(new YeelightProviderMetadata())
        {

        }

        public override async Task<List<Provider.Device>> Discover()
        {
            var yeelightInternalDevices = await DeviceLocator.DiscoverAsync();
            return yeelightInternalDevices.Select(device => new YeelightDevice(ProviderMetadata.ProviderGuid, device)).ToList<Provider.Device>();
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
