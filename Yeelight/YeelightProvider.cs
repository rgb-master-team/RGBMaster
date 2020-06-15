using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;

namespace Yeelight
{
    public class YeelightProvider : Provider<YeelightProviderMetadata, YeelightDeviceMetadata>
    {
        public override async Task<IEnumerable<Device<YeelightDeviceMetadata>>> Discover()
        {
            return (await DeviceLocator.Discover()).Select(device => new YeelightDevice(device));
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
