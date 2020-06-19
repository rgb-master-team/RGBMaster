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
        private List<Light> internalDiscoveredDevices;
        private bool isInitialized = false;

        public MagicHomeProvider(): base(new MagicHomeProviderMetadata())
        {

        }

        public override Task<IEnumerable<Device>> Discover()
        {
            List<Light> internalDevices;

            if (!isInitialized)
            {
                internalDevices = Light.Discover();
                internalDiscoveredDevices = internalDevices;
                isInitialized = true;
            }
            else
            {
                internalDevices = internalDiscoveredDevices;
            }

            return Task.FromResult<IEnumerable<Device>>(internalDevices.Select(internalDevice => new MagicHomeDevice(internalDevice)));
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
