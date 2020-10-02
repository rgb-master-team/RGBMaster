using Colore;
using Provider;
using RazerChroma.Devices.AllDevices;
using RazerChroma.Devices.Keyboard;
using RazerChroma.Devices.Mousepad;
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
        private static int VENDOR_ID = 0x1532;

        public RazerChromaProvider(): base(new RazerChromaProviderMetadata())
        {

        }

        public override Task<List<Device>> Discover()
        {
            return Task.FromResult(new List<Device> { new RazerChromaAllDevicesDevice(internalChromaProvider), new RazerChromaKeyboardDevice(internalChromaProvider), new RazerChromaMousepadDevice(internalChromaProvider) });
        }

        protected async override Task InternalRegister()
        {
            internalChromaProvider = await ColoreProvider.CreateNativeAsync();
            await internalChromaProvider.InitializeAsync(new Colore.Data.AppInfo("RGBMaster", "Syncs your brothers and sisters into the light", "RGBMasters", "RGBMaster@github", Colore.Data.Category.Application));
        }

        protected async override Task InternalUnregister()
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
