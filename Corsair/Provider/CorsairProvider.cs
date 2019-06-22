using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;

namespace Corsair.Provider
{
	public class CorsairProvider : Infrastructure.Provider
	{
		public override string ProviderName => "Corsair Sync";
		public override Task Register()
		{
			CUESDK.CUESDK.PerformProtocolHandshake();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

		public override Task<IEnumerable<Infrastructure.Device>> Discover()
		{
			return Task.FromResult<IEnumerable<Infrastructure.Device>>(CUESDK.CUESDK.GetAllDevices());
		}
	}
}
