using Common;
using NZXT.Devices;
using NZXTSharp.HuePlus;
using NZXTSharp.KrakenX;
using Provider;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
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
            var devices = new List<Device>();

            if (huePlusDevice != null)
            {
                devices.Add(new NZXTHuePlusDevice(ProviderMetadata.ProviderGuid, huePlusDevice));
            }

            if (krakenXDevice != null)
            {
                devices.Add(new NZXTKrakenXDevice(ProviderMetadata.ProviderGuid, krakenXDevice));
            }

            return Task.FromResult(devices);
        }

        protected override Task InternalRegister()
        {
            try
            {
                huePlusDevice = new HuePlus();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex, "Could not detect a HuePlus device via HID.");
            }

            try
            {
                krakenXDevice = new KrakenX();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex, "Could not detect a KrakenX device via HID.");
            }

            return Task.CompletedTask;
        }

        protected override Task InternalUnregister()
        {
            huePlusDevice.Dispose();
            krakenXDevice.Dispose();
            return Task.CompletedTask;
        }
    }
}
