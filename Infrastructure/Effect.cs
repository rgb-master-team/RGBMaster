using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Effect
    {
        protected IEnumerable<Device> devices { get; private set; } = new List<Device>();
        private SemaphoreSlim changeConnectedDevicesSemaphore = new SemaphoreSlim(1, 1);

        public async Task ChangeConnectedDevices(IEnumerable<Device> devices)
        {
            var addedDevices = devices.Except(this.devices);
            var removedDevices = this.devices.Except(devices);

            await changeConnectedDevicesSemaphore.WaitAsync();
            
            await DisconnectDevices(removedDevices);
            await ConnectDevices(addedDevices);

            changeConnectedDevicesSemaphore.Release();

            this.devices = new List<Device>(devices);
        }

        private async Task DisconnectDevices(IEnumerable<Device> devicesToDisconnect)
        {
            var connectionTasks = new List<Task>();

            foreach (var removedDevice in devicesToDisconnect)
            {
                connectionTasks.Add(removedDevice.Disconnect());
            }

            await Task.WhenAll(connectionTasks);
        }

        private async Task ConnectDevices(IEnumerable<Device> devicesConnect)
        {
            var connectionTasks = new List<Task>();

            foreach (var removedDevice in devicesConnect)
            {
                connectionTasks.Add(removedDevice.Connect());
            }

            await Task.WhenAll(connectionTasks);
        }

        public async Task Start()
        {
            await StartInternal();
        }

        public async Task Stop()
        {
            foreach (var device in devices)
            {
                await device.Disconnect();
            }

            await StopInternal();

            devices = Enumerable.Empty<Device>();
        }

        public abstract Task StopInternal();
        public abstract Task StartInternal();
    }
}
