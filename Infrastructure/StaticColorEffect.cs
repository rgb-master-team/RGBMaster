using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StaticColorEffect : IEffect
    {
        private IEnumerable<Device> currentDevices;
        private System.Drawing.Color currentColor;

        public async Task ChangeStaticColor(System.Drawing.Color newColor)
        {
            var tasksList = new List<Task>();

            foreach (var device in currentDevices)
            {
                tasksList.Add(Task.Run(() => device.SetColor(newColor)));
            }

            await Task.WhenAll(tasksList);
        }

        public async Task Start(IEnumerable<Device> devices)
        {
            currentDevices = devices;

            await ChangeStaticColor(currentColor);
        }

        public Task Stop()
        {
            return Task.CompletedTask;
        }
    }
}
