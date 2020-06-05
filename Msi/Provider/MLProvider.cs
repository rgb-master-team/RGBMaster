using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Msi.SDKs;

namespace Msi.Provider
{
	public class MLProvider : Infrastructure.Provider
	{
		public override string ProviderName => "MSI Sync";

		protected override Task Register()
		{
			MysticLightSdk.Initialize();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

		public override Task<IEnumerable<Device>> Discover()
		{
			return Task.FromResult<IEnumerable<Device>>(MysticLightSdk.GetAllDevices());
		}
	}
}
