using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using Device = Infrastructure.Device;

namespace Yeelight
{
    public class YeelightProvider : Provider
    {
        public override string ProviderName => "Xiaomi Yeelight";

        public override async Task<IEnumerable<Device>> Discover()
        {
            return (await DeviceLocator.Discover()).Select(device => new YeelightDevice(device));
        }

        public override Task Register()
        {
            return Task.CompletedTask;
        }

        public override Task Unregister()
        {
            return Task.CompletedTask;
        }
    }
}
