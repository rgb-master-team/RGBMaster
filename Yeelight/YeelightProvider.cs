using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeelight
{
    public class YeelightProvider : Provider
    {
        private readonly List<OperationType> yeelightSupportedOps = new List<OperationType>() { OperationType.Connect, OperationType.Disconnect, OperationType.GetBrightness, OperationType.SetBrightness, OperationType.GetColor, OperationType.SetColor, OperationType.SetPower };

        public override IEnumerable<OperationType> SupportedOperations => yeelightSupportedOps;
        public override Task<IEnumerable<Device>> Discover()
        {
            throw new NotImplementedException();
        }

        public override Task SendRequest(OperationType requestType, IEnumerable<Device> devices, IEnumerable<object> args)
        {
            List<Task> executionTasks = new List<Task>();
            foreach (var device in devices)
            {
                executionTasks.Add(device.ExecuteRequest(requestType, args));
            }

            return Task.WhenAll(executionTasks);
        }
    }
}
