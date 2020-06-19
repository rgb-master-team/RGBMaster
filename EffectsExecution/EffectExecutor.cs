using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public abstract class EffectExecutor
    {
        public readonly EffectMetadata executedEffectMetadata;

        public EffectExecutor(EffectMetadata executedEffectMetadata)
        {
            this.executedEffectMetadata = executedEffectMetadata;
        }

        protected IEnumerable<Device> Devices { get; private set; } = new List<Device>();
        private SemaphoreSlim changeConnectedDevicesSemaphore = new SemaphoreSlim(1, 1);

        public void ChangeConnectedDevices(IEnumerable<Device> devices)
        {
            //var addedDevices = devices.Except(this.Devices);
            //var removedDevices = this.Devices.Except(devices);

            //await changeConnectedDevicesSemaphore.WaitAsync();

            //await DisconnectDevices(removedDevices);
            //await ConnectDevices(addedDevices);

            //changeConnectedDevicesSemaphore.Release();

            this.Devices = new List<Device>(devices);
        }

        //private async Task DisconnectDevices(IEnumerable<Device> devicesToDisconnect)
        //{
        //    var connectionTasks = new List<Task>();

        //    foreach (var removedDevice in devicesToDisconnect)
        //    {
        //        connectionTasks.Add(removedDevice.Disconnect());
        //    }

        //    await Task.WhenAll(connectionTasks);
        //}

        //private async Task ConnectDevices(IEnumerable<Device> devicesConnect)
        //{
        //    var connectionTasks = new List<Task>();

        //    foreach (var removedDevice in devicesConnect)
        //    {
        //        connectionTasks.Add(removedDevice.Connect());
        //    }

        //    await Task.WhenAll(connectionTasks);
        //}

        public async Task Start()
        {
            await StartInternal();
        }

        public async Task Stop()
        {
            foreach (var device in Devices)
            {
                await device.Disconnect();
            }

            await StopInternal();

            Devices = Enumerable.Empty<Device>();
        }

        public abstract Task StopInternal();
        public abstract Task StartInternal();
    }
}
