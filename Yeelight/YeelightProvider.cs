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

        public override async Task<IEnumerable<Provider.Device>> Discover()
        {
            var yeelightInternalDevices = await DeviceLocator.Discover();
            return yeelightInternalDevices.Select(device => new YeelightDevice(device)).ToList();
        }

        protected override Task Register()
        {
            return Task.CompletedTask;
        }

        public override Task Unregister()
        {
            return Task.CompletedTask;
        }
    }
}
