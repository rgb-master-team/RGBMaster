using Corsair.Provider;
using Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorsairProvider
{
    public class CorsairProvider : BaseProvider
    {
        public CorsairProvider() : base(new CorsairProviderMetadata())
        {
        }

        protected override Task Register()
		{
			Corsair.CUESDK.CUESDK.PerformProtocolHandshake();

			return Task.CompletedTask;
		}

		public override Task Unregister()
		{
			return Task.CompletedTask;
		}

        public override Task<IEnumerable<Device>> Discover()
        {
			var devices = Corsair.CUESDK.CUESDK.GetAllDevices();
			return Task.FromResult<IEnumerable<Device>>(devices);
		}
	}
}
