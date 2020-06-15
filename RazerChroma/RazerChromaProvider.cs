using Colore;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaProvider : Provider<RazerChromaProviderMetadata, RazerChromaDeviceMetadata>
    {
        private IChroma internalChromaProvider;

        public override Task<IEnumerable<Device<RazerChromaDeviceMetadata>>> Discover()
        {
            return Task.FromResult<IEnumerable<Device<RazerChromaDeviceMetadata>>>(new List<RazerChromaDevice>(1) { new RazerChromaDevice(internalChromaProvider) });
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
