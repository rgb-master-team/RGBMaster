using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

		protected override Task InternalRegister(CancellationToken cancellationToken = default)
		{
			MysticLightSdk.Initialize();

			return Task.CompletedTask;
		}

		protected override Task InternalUnregister(CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

		protected override Task<List<RGBProvider.Device>> InternalDiscover(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(MysticLightSdk.GetAllDevices(ProviderMetadata.ProviderGuid).ToList<RGBProvider.Device>());
		}
	}
}
