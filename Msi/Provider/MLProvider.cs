using System.Collections.Generic;
using System.Threading.Tasks;
using Msi.SDKs;
using RGBProvider = Provider;

namespace Msi.Provider
{
    public class MLProvider : RGBProvider.BaseProvider
	{
        public MLProvider(): base(new MLProviderMetadata())
        {

        }

		protected override Task Register()
		{
			MysticLightSdk.Initialize();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

		public override Task<IEnumerable<RGBProvider.Device>> Discover()
		{
			return Task.FromResult<IEnumerable<RGBProvider.Device>>(MysticLightSdk.GetAllDevices());
		}
	}
}
