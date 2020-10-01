using Corsair.Provider;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsairProvider
{
    public class CorsairProvider : BaseProvider
    {
        public CorsairProvider() : base(new CorsairProviderMetadata())
        {
        }

        protected override Task InternalRegister()
		{
			Corsair.CUESDK.CUESDK.PerformProtocolHandshake();

			return Task.CompletedTask;
		}

		public override Task InternalUnregister()
		{
			return Task.CompletedTask;
		}

        public override Task<List<Device>> Discover()
        {
			var devices = Corsair.CUESDK.CUESDK.GetAllDevices();
			return Task.FromResult(devices.ToList<Device>());
		}
	}
}
