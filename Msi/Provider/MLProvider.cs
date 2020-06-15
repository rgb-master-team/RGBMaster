using System.Collections.Generic;
using System.Threading.Tasks;
using Msi.SDKs;
using RGBProvider = Provider;

namespace Msi.Provider
{
    public class MLProvider : RGBProvider.Provider<MLProviderMetadata, MLDeviceMetadata>
	{
		protected override Task Register()
		{
			MysticLightSdk.Initialize();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

		public override Task<IEnumerable<RGBProvider.Device<MLDeviceMetadata>>> Discover()
		{
			return Task.FromResult<IEnumerable<RGBProvider.Device<MLDeviceMetadata>>>(MysticLightSdk.GetAllDevices());
		}
	}
}
