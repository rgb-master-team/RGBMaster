using Corsair.CUESDK;
using Corsair.Device;
using Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corsair.Provider
{
    public class CorsairProvider : Provider<CorsairProviderMetadata, CorsairDeviceMetadata>
    {
        public CorsairProvider() : base(new CorsairProviderMetadata())
        {
        }

        protected override Task Register()
		{
			CUESDK.CUESDK.PerformProtocolHandshake();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

        public override Task<IEnumerable<Device<CorsairDeviceMetadata>>> Discover()
        {
			var devices = CUESDK.CUESDK.GetAllDevices();
			return Task.FromResult<IEnumerable<Device<CorsairDeviceMetadata>>>(devices);
		}
	}
}
