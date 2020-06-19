using Colore;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaProvider : BaseProvider
    {
        private IChroma internalChromaProvider;

        public RazerChromaProvider(): base(new RazerChromaProviderMetadata())
        {

        }

        public override Task<IEnumerable<Device>> Discover()
        {
            return Task.FromResult<IEnumerable<Device>>(new List<RazerChromaDevice>(1) { new RazerChromaDevice(internalChromaProvider) });
        }

        protected async override Task Register()
        {
            internalChromaProvider = await ColoreProvider.CreateNativeAsync();
            //internalChromaProvider = await ColoreProvider.CreateRestAsync(new Colore.Data.AppInfo("RGBMaster", "Apply effects to RGB peripherals", "RGBMaster", "RGBMaster", Colore.Data.Category.Application));
        }

        public async override Task Unregister()
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
