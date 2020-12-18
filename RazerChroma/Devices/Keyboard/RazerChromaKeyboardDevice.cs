using Colore;
using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using ColoreColor = Colore.Data.Color;

namespace RazerChroma.Devices.Keyboard
{
    public class RazerChromaKeyboardDevice : Device
    {
        private readonly IChroma internalChromaDriver;

        public RazerChromaKeyboardDevice(IChroma chroma, RazerChromaKeyboardDeviceMetadata razerChromaKeyboardDeviceMetadata) : base(razerChromaKeyboardDeviceMetadata)
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
            await internalChromaDriver.Keyboard.SetStaticAsync(new Colore.Effects.Keyboard.StaticKeyboardEffect(new ColoreColor(color.R, color.G, color.B))).ConfigureAwait(false);
        }

        protected override Task SetColorSmoothlyInternal(Color color, int relativeSmoothness)
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
