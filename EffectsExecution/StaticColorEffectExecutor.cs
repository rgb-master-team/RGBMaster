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

        private async Task ChangeStaticColor(StaticColorEffectProps newStaticColorEffectProps)
        {
            var tasksList = new List<Task>();

            foreach (var device in Devices)
            {
                tasksList.Add(Task.Run(() =>
                {
                    device.SetColor(newStaticColorEffectProps.SelectedColor);
                    device.SetBrightnessPercentage(newStaticColorEffectProps.SelectedBrightness);
                }));
            }

            await Task.WhenAll(tasksList);
        }

        protected override async Task StartInternal()
        {
            await ChangeStaticColor(AppState.Instance.StaticColorEffectProperties);
        }

        protected override Task StopInternal()
        {
            return Task.CompletedTask;
        }
    }
}
