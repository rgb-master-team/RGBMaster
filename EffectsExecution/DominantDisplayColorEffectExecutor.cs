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
            Task.Run(async () => await DoWork(enumeratedDevices), backgroundWorkCancellationTokenSource.Token).ConfigureAwait(false);

            return Task.CompletedTask;
        }

        private async Task DoWork(List<Device> devices)
        {
            while (!backgroundWorkCancellationTokenSource.IsCancellationRequested)
            {
                var effectProps = ((DominantDisplayColorEffectMetadata)executedEffectMetadata).EffectProperties;

                bool shouldSyncBrightnessByHSL = effectProps.SyncBrightnessByHSL;

                var smoothness = effectProps.RelativeSmoothness;

                var color = GfxUtils.GetDominantColorFromThief();

                List<Task> setColorTasks = new List<Task>();

                byte desiredBrightnessPercentage = 0;

                if (shouldSyncBrightnessByHSL)
                {
                    desiredBrightnessPercentage = (byte)(color.GetBrightness() * 100);
                }

                foreach (var device in devices)
                {
                    if (smoothness > 0 && device.DeviceMetadata.IsOperationSupported(OperationType.SetColorSmoothly))
                    {
                        setColorTasks.Add(Task.Run(async () => await device.SetColorSmoothly(color, smoothness).ConfigureAwait(false)));
                    }
                    else if (device.DeviceMetadata.IsOperationSupported(OperationType.SetColor))
                    {
                        setColorTasks.Add(Task.Run(async () => await device.SetColor(color).ConfigureAwait(false)));
                    }

                    if (shouldSyncBrightnessByHSL && device.DeviceMetadata.IsOperationSupported(OperationType.SetBrightness))
                    {
                        setColorTasks.Add(Task.Run(async () => await device.SetBrightnessPercentage(desiredBrightnessPercentage).ConfigureAwait(false)));
                    }
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
