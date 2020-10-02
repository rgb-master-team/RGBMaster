using System.Collections.Generic;
using System.Linq;
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

		protected override Task InternalRegister()
		{
			MysticLightSdk.Initialize();

			return Task.CompletedTask;
		}

		protected override Task InternalUnregister()
		{
			return Task.CompletedTask;
		}

		public override Task<List<RGBProvider.Device>> Discover()
		{
			return Task.FromResult(MysticLightSdk.GetAllDevices().ToList<RGBProvider.Device>());
		}
	}
}
