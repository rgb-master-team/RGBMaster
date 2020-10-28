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

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override async Task SetColorInternal(Color color)
        {
            await internalChromaDriver.Mousepad.SetStaticAsync(new Colore.Effects.Mousepad.StaticMousepadEffect(new Colore.Data.Color(color.R, color.G, color.B))).ConfigureAwait(false);
        }

        protected override Task SetGradientInternal(GradientPoint gradientPoint, int relativeSmoothness)
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
