using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class GradientEffectExecutor : EffectExecutor
    {
        private CancellationTokenSource cancellationTokenSource;

        public GradientEffectExecutor() : base(new GradientEffectMetadata())
        {
        }

        protected override Task StartInternal()
        {
            _ = DoWork();
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            cancellationTokenSource = new CancellationTokenSource();

            var effectProps = ((GradientEffectMetadata)executedEffectMetadata).EffectProperties;

            if (effectProps.GradientPoints.Count == 0)
            {
                await StopInternal();
            }

            var index = 0;

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                var tasks = new List<Task>();

                var currentGradientPoint = effectProps.GradientPoints[index];

                foreach (var device in Devices)
                {
                    tasks.Add(Task.Run(async () => await device.SetGradient(currentGradientPoint, effectProps.RelativeSmoothness)));
                }

                await Task.WhenAll(tasks).ContinueWith(async (_) => await Task.Delay(TimeSpan.FromMilliseconds(effectProps.RelativeSmoothness + effectProps.DelayInterval))).Unwrap();

                index = index == effectProps.GradientPoints.Count - 1 ? 0 : index + 1;
            }
        }

        protected override Task StopInternal()
        {
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
