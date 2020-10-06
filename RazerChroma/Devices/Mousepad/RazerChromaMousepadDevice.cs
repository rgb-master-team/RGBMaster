using Colore;
using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma.Devices.Mousepad
{
    public class RazerChromaMousepadDevice : Device
    {
        private readonly IChroma internalChromaDriver;

        public RazerChromaMousepadDevice(IChroma chroma, RazerChromaMousepadDeviceMetadata razerChromaMousepadDeviceMetadata) : base(razerChromaMousepadDeviceMetadata)
        {
            internalChromaDriver = chroma;
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

        protected override Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected async override void SetColorInternal(Color color)
        {
            await internalChromaDriver.Mousepad.SetStaticAsync(new Colore.Effects.Mousepad.StaticMousepadEffect(new Colore.Data.Color(color.R, color.G, color.B)));
        }

        protected override void TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override void TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
