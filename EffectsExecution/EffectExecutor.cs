using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public abstract class EffectExecutor
    {
        public readonly EffectMetadata executedEffectMetadata;

        public EffectExecutor(EffectMetadata executedEffectMetadata)
        {
            this.executedEffectMetadata = executedEffectMetadata;
        }

        protected IEnumerable<Device> Devices { get; private set; } = new List<Device>();

        public void ChangeConnectedDevices(IEnumerable<Device> devices)
        {
            Devices = new List<Device>(devices);
        }

        public async Task Start()
        {
            await StartInternal();
        }

        public async Task Stop()
        {
            await StopInternal();
        }

        public abstract Task StopInternal();
        public abstract Task StartInternal();
    }
}
