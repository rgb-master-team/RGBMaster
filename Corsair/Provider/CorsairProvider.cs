using Corsair.Provider;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CorsairProvider
{
    public class CorsairProvider : BaseProvider
    {
		private static int VENDOR_ID = 0x1B1C;
		public CorsairProvider() : base(new CorsairProviderMetadata())
        {
        }

        protected override Task InternalRegister(CancellationToken cancellationToken = default)
		{
			Corsair.CUESDK.CUESDK.PerformProtocolHandshake();

			return Task.CompletedTask;
		}

		protected override Task InternalUnregister(CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

        public override Task<List<Device>> Discover()
        {
			var devices = Corsair.CUESDK.CUESDK.GetAllDevices(ProviderMetadata.ProviderGuid);
			return Task.FromResult(devices.ToList<Device>());
		}
	}
}
