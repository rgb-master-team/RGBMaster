using Colore;
using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma.Devices.AllDevices
{
    public class RazerChromaAllDevicesDevice : Device
    {
        private readonly IChroma internalChromaDriver;

        public RazerChromaAllDevicesDevice(IChroma internalChromaDriver, RazerChromaAllDevicesDeviceMetadata razerChromaAllDevicesDeviceMetadata) : base(razerChromaAllDevicesDeviceMetadata)
        {
            this.internalChromaDriver = internalChromaDriver;
        }

        protected override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<System.Drawing.Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override async Task SetColorInternal(System.Drawing.Color color)
        {
            await internalChromaDriver.SetAllAsync(new Colore.Data.Color(color.R, color.G, color.B)).ConfigureAwait(false);
        }

        protected override Task SetGradientInternal(GradientPoint gradientPoint)
        {
            throw new NotImplementedException();
        }

        protected override async Task TurnOffInternal()
        {
            await internalChromaDriver.SetAllAsync(new Colore.Data.Color(0, 0, 0)).ConfigureAwait(false);
        }

        protected override async Task TurnOnInternal()
        {
            await internalChromaDriver.SetAllAsync(new Colore.Data.Color(255, 255, 255)).ConfigureAwait(false);
        }
    }
}
