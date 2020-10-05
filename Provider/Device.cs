using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Provider
{
    public abstract class Device
    {
        public readonly DeviceMetadata DeviceMetadata;

        public Device(DeviceMetadata deviceMetadata)
        {
            DeviceMetadata = deviceMetadata;
        }

        public bool IsConnected { get; private set; }
        public bool IsTurnedOn { get; private set; }

        public Color GetColor()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.GetColor))
            {
                return GetColorInternal();
            }

            return Color.Empty;
        }

        protected abstract Color GetColorInternal();

        public void SetColor(Color color)
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.SetColor))
            {
                SetColorInternal(color);
            }
        }

        protected abstract void SetColorInternal(Color color);

        public byte GetBrightnessPercentage()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.GetBrightness))
            {
               return GetBrightnessPercentageInternal();
            }

            return 0;
        }

        protected abstract byte GetBrightnessPercentageInternal();

        public void SetBrightnessPercentage(byte brightness)
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.SetBrightness))
            {
                SetBrightnessPercentageInternal(brightness);
            }
        }

        protected abstract void SetBrightnessPercentageInternal(byte brightness);

        public void TurnOn()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.TurnOn))
            {
                TurnOnInternal();
                IsTurnedOn = true;
            }
        }

        public void TurnOff()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.TurnOff))
            {
                TurnOffInternal();
                IsTurnedOn = false;
            }
        }

        protected abstract void TurnOnInternal();
        protected abstract void TurnOffInternal();

        public async Task<bool> Connect()
        {
            bool didSucceed;

            if (IsConnected)
            {
                didSucceed = true;
            }
            else
            {
                try
                {
                    // TODO - Move to a CancellationToken mechanism and enforce receiving CancellationTokens
                    // in all ConnectInternal methods.
                    var connectTimeoutSpan = TimeSpan.FromSeconds(10);
                    await ConnectInternal().TimeoutAfter(connectTimeoutSpan, $"Failed to connect to device {DeviceMetadata.DeviceName} with guid {DeviceMetadata.RgbMasterDeviceGuid}").ConfigureAwait(false);

                    IsConnected = true;
                    didSucceed = true;
                }
                catch (Exception)
                {
                    // TODO - Log exceptions here.
                    IsConnected = false;
                    didSucceed = false;
                }
            }

            return didSucceed;
        }

        public async Task<bool> Disconnect()
        {
            bool didSucceed;

            if (!IsConnected)
            {
                didSucceed = true;
            }
            else
            {
                try
                {
                    // TODO - Move to a cancellationtoken mechanism and enforce receiving cancellationtokens
                    // in all DisconnectInternal methods.
                    var disconnectTimeoutSpan = TimeSpan.FromSeconds(10);
                    await DisconnectInternal().TimeoutAfter(disconnectTimeoutSpan, $"Failed to disconnect from device {DeviceMetadata.DeviceName} with guid {DeviceMetadata.RgbMasterDeviceGuid}").ConfigureAwait(false);

                    IsConnected = false;
                    didSucceed = true;
                }
                catch (Exception)
                {
                    // TODO - Log
                    IsConnected = false;
                    didSucceed = false;
                }
            }

            return didSucceed;
        }

        protected abstract Task ConnectInternal();
        protected abstract Task DisconnectInternal();
    }
}
