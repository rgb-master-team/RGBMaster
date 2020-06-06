using Colore;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaProvider : Provider
    {
        private IChroma internalChromaProvider;

        public override string ProviderName => "Razer Chroma";

        public override Task<IEnumerable<Device>> Discover()
        {
            return Task.FromResult<IEnumerable<Device>>(new List<Device>(1) { new RazerChromaDevice(internalChromaProvider) });
        }

        protected async override Task Register()
        {
            //internalChromaProvider = await ColoreProvider.CreateNativeAsync();
            internalChromaProvider = await ColoreProvider.CreateRestAsync(new Colore.Data.AppInfo("RGBMaster", "Apply effects to RGB peripherals", "RGBMaster", "RGBMaster", Colore.Data.Category.Application));
        }

        public async override Task Unregister()
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
