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

        public async override Task<IEnumerable<Device>> Discover()
        {
            return new List<Device>(1) { new RazerChromaDevice(internalChromaProvider) };
        }

        public async override Task Register()
        {
            internalChromaProvider = await ColoreProvider.CreateNativeAsync();
        }

        public async override Task Unregister()
        {
            internalChromaProvider.Unregister();
            await internalChromaProvider.UninitializeAsync();
        }
    }
}
