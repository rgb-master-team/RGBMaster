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

        public RazerChromaKeyboardDevice(IChroma chroma) : base(new RazerChromaKeyboardDeviceMetadata())
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
            await internalChromaDriver.Keyboard.SetStaticAsync(new Colore.Effects.Keyboard.StaticKeyboardEffect(new ColoreColor(color.R, color.G, color.B)));
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
