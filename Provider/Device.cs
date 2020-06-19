using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public abstract class Device
    {
        public readonly DeviceMetadata DeviceMetadata;

        public Device(DeviceMetadata deviceMetadata)
        {
            this.DeviceMetadata = deviceMetadata;
        }

        public bool IsConnected { get; private set; }
        public bool IsTurnedOn { get; private set; }
        public abstract Color GetColor();
        public abstract void SetColor(Color color);
        public abstract byte GetBrightnessPercentage();
        public abstract void SetBrightnessPercentage(byte brightness);

        public void TurnOn()
        {
            TurnOnInternal();
            IsTurnedOn = true;
        }

        public void TurnOff()
        {
            TurnOffInternal();
            IsTurnedOn = false;
        }

        public abstract void TurnOnInternal();
        public abstract void TurnOffInternal();

        public async Task Connect()
        {
            if (IsConnected)
            {
                return;
            }

            await ConnectInternal();
            IsConnected = true;
        }

        public async Task Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }

            await DisconnectInternal();
            IsConnected = false;
        }

        public abstract Task ConnectInternal();
        public abstract Task DisconnectInternal();
    }
}
