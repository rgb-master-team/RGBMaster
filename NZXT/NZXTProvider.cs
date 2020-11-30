using Common;
using NZXT.Devices;
using NZXTSharp.HuePlus;
using NZXTSharp.KrakenX;
using Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NZXT
{
    public class NZXTProvider : BaseProvider
    {
        private HuePlus huePlusDevice;
        private KrakenX krakenXDevice;

        public NZXTProvider() : base(new NZXTProviderMetadata())
        {
        }
        public override Task<List<Device>> Discover()
        {
            var nzxtHuePlusDevice = new NZXTHuePlusDevice(ProviderMetadata.ProviderGuid, huePlusDevice);
            var nzxtKrakenXDevice = new NZXTKrakenXDevice(ProviderMetadata.ProviderGuid, krakenXDevice);

            return Task.FromResult(new List<Device>() { nzxtHuePlusDevice, nzxtKrakenXDevice });
        }

        protected override Task InternalRegister(CancellationToken cancellationToken = default)
        {
            huePlusDevice = new HuePlus();
            krakenXDevice = new KrakenX();
            return Task.CompletedTask;
        }

        protected override Task InternalUnregister(CancellationToken cancellationToken = default)
        {
            huePlusDevice.Dispose();
            krakenXDevice.Dispose();
            return Task.CompletedTask;
        }
    }
}
