using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class StaticColorEffectExecutor : EffectExecutor
    {
        public StaticColorEffectExecutor() : base(new StaticColorEffectMetadata())
        {
        }

        private async Task ChangeStaticColor()
        {
            var tasksList = new List<Task>();

            foreach (var device in Devices)
            {
                var newStaticColorEffectProps = ((StaticColorEffectMetadata)executedEffectMetadata).EffectProperties;

                var smoothness = newStaticColorEffectProps.RelativeSmoothness;
                var color = newStaticColorEffectProps.SelectedColor;

                if (smoothness > 0 && device.DeviceMetadata.IsOperationSupported(OperationType.SetColorSmoothly))
                {
                    tasksList.Add(Task.Run(async () =>
                    {
                        await device.SetColorSmoothly(color, smoothness);
                    }));
                }
                else
                {
                    tasksList.Add(Task.Run(async () =>
                    {
                        await device.SetColor(color);
                    }));
                }

                tasksList.Add(Task.Run(async () =>
                {
                    await device.SetBrightnessPercentage(newStaticColorEffectProps.SelectedBrightness);
                }));
            }

            await Task.WhenAll(tasksList);
        }

        protected override async Task StartInternal()
        {
            await ChangeStaticColor();
        }

        protected override Task StopInternal()
        {
            return Task.CompletedTask;
        }
    }
}
