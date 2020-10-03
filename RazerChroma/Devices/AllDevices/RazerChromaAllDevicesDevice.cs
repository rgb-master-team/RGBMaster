using Colore;
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

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected async override void SetColorInternal(System.Drawing.Color color)
        {
            await internalChromaDriver.SetAllAsync(new Colore.Data.Color(color.R, color.G, color.B));
        }

        protected override void TurnOffInternal()
        {
            internalChromaDriver.SetAllAsync(new Colore.Data.Color(0, 0, 0)).Wait(10000);
        }

        protected override void TurnOnInternal()
        {
            internalChromaDriver.SetAllAsync(new Colore.Data.Color(255, 255, 255)).Wait(10000);
        }
    }
}
