using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YeelightAPI;

namespace Yeelight
{
    public class YeelightProvider : BaseProvider
    {
        public YeelightProvider(): base(new YeelightProviderMetadata())
        {

        }

        protected override async Task<List<Provider.Device>> InternalDiscover(CancellationToken cancellationToken = default)
        {
            DeviceLocator.UseAllAvailableMulticastAddresses = true;
            var yeelightInternalDevices = await DeviceLocator.DiscoverAsync(cancellationToken);

            return yeelightInternalDevices.Select(device => new YeelightDevice(ProviderMetadata.ProviderGuid, device)).ToList<Provider.Device>();
        }

        protected override Task InternalRegister(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
