using Colore;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaDevice : Device
    {
        private readonly HashSet<OperationType> chromaSupportedOps = new HashSet<OperationType>() { OperationType.SetColor };

        public override string DeviceName => "All Razer Chroma connected devices";

        public override HashSet<OperationType> SupportedOperations => chromaSupportedOps;

        private readonly IChroma internalChromaDriver;

        public RazerChromaDevice(IChroma internalChromaDriver)
        {
            this.internalChromaDriver = internalChromaDriver;
        }

        public override Task Connect()
        {
            return Task.CompletedTask;
        }

        public override Task Disconnect()
        {
            internalChromaDriver.Unregister();
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
    }
}
