using Colore;
using Provider;
using RazerChroma.Devices;
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

        public override Task<List<Device>> Discover()
        {
            //return Task.FromResult(new List<Device>(1) { new RazerChromaDevice(internalChromaProvider) });
            return Task.FromResult(new List<Device> { new RazerChromaMousepadDevice(internalChromaProvider) });
        }

        protected async override Task Register()
        {
            internalChromaProvider = await ColoreProvider.CreateNativeAsync();
            await internalChromaProvider.InitializeAsync(new Colore.Data.AppInfo("RGBMaster", "Syncs your brothers and sisters into the light", "RGBMasters", "RGBMaster@github", Colore.Data.Category.Application));
            //internalChromaProvider = await ColoreProvider.CreateRestAsync(new Colore.Data.AppInfo("RGBMaster", "Apply effects to RGB peripherals", "RGBMaster", "RGBMaster", Colore.Data.Category.Application));
        }

        public async override Task Unregister()
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
