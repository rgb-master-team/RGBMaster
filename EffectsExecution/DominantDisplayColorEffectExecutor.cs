using Common;
using EffectsExecution.Utils;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class DominantDisplayColorEffectExecutor : EffectExecutor
    {
        private CancellationTokenSource backgroundWorkCancellationTokenSource;

        public DominantDisplayColorEffectExecutor() : base(new DominantDisplayColorEffectMetadata())
        {
        }

        protected override Task StartInternal()
        {
            var enumeratedDevices = Devices.ToList();

            backgroundWorkCancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await DoWork(enumeratedDevices), backgroundWorkCancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        private async Task DoWork(List<Device> devices)
        {
            while (!backgroundWorkCancellationTokenSource.IsCancellationRequested)
            {
                var c = GfxUtils.GetDominantColorFromThief();

                List<Task> setColorTasks = new List<Task>();

                foreach (var device in devices)
                {
                    setColorTasks.Add(Task.Run(async () => await device.SetColor(c)));
                }

                await Task.WhenAll(setColorTasks);
            }
        }

        protected override Task StopInternal()
        {
            backgroundWorkCancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
