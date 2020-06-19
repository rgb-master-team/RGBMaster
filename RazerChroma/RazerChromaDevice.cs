using Colore;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaDevice : Device
    {
        private readonly IChroma internalChromaDriver;

        public RazerChromaDevice(IChroma internalChromaDriver): base(new RazerChromaDeviceMetadata())
        {
            this.internalChromaDriver = internalChromaDriver;
        }

        public override Task ConnectInternal()
        {
            return Task.CompletedTask;
        }

        public override Task DisconnectInternal()
        {
            return Task.CompletedTask;
        }

        public override byte GetBrightnessPercentage()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Color GetColor()
        {
            throw new NotImplementedException();
        }

        public override void SetBrightnessPercentage(byte brightness)
        {
            throw new NotImplementedException();
        }

        public async override void SetColor(System.Drawing.Color color)
        {
            await internalChromaDriver.SetAllAsync(new Colore.Data.Color(color.R, color.G, color.B));
        }

        public override void TurnOffInternal()
        {
            throw new NotImplementedException();
        }

        public override void TurnOnInternal()
        {
            throw new NotImplementedException();
        }
    }
}
