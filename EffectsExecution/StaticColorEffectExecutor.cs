using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class StaticColorEffectExecutor : EffectExecutor<StaticColorEffectMetadata>
    {
        private async Task ChangeStaticColor(System.Drawing.Color newColor)
        {
            var tasksList = new List<Task>();

            foreach (var device in devices)
            {
                tasksList.Add(Task.Run(() => device.SetColor(newColor)));
            }

            await Task.WhenAll(tasksList);
        }

        public override async Task StartInternal()
        {
            await ChangeStaticColor(executedEffectMetadata.EffectProperties.SelectedColor);
        }

        public override Task StopInternal()
        {
            return Task.CompletedTask;
        }
    }
}
