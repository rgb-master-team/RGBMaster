using Common;
using EffectsExecution.Utils;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class CursorColorEffectExecutor : EffectExecutor
    {
        private CancellationTokenSource backgroundWorkCancellationTokenSource;

        public CursorColorEffectExecutor() : base(new CursorColorEffectMetadata())
        {
        }

        protected override Task StartInternal()
        {
            var enumeratedDevices = Devices.ToList();

            backgroundWorkCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => DoWork(enumeratedDevices), backgroundWorkCancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        private async void DoWork(List<Device> devices)
        {
            while (!backgroundWorkCancellationTokenSource.IsCancellationRequested)
            {
                var smoothness = ((CursorColorEffectMetadata)executedEffectMetadata).EffectProperties.RelativeSmoothness;

                var cursorLoc = GfxUtils.GetCursorLocation();

                var color = GfxUtils.GetColorAt(cursorLoc.X, cursorLoc.Y);

                List<Task> setColorTasks = new List<Task>();

                foreach (var device in devices)
                {
                    if (smoothness > 0 && device.DeviceMetadata.IsOperationSupported(OperationType.SetColorSmoothly))
                    {
                        setColorTasks.Add(Task.Run(async () => await device.SetColorSmoothly(color, smoothness)));
                    }
                    else
                    {
                        setColorTasks.Add(Task.Run(async () => await device.SetColor(color)));
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
