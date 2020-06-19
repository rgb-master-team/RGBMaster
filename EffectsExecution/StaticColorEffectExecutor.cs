using Common;
using RGBMasterUWPApp.State;
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

        private async Task ChangeStaticColor(System.Drawing.Color newColor)
        {
            var tasksList = new List<Task>();

            foreach (var device in Devices)
            {
                tasksList.Add(Task.Run(() => device.SetColor(newColor)));
            }

            await Task.WhenAll(tasksList);
        }

        public override async Task StartInternal()
        {
            await ChangeStaticColor(AppState.Instance.StaticColor);
        }

        public override Task StopInternal()
        {
            return Task.CompletedTask;
        }
    }
}
