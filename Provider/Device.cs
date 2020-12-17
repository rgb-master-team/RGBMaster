using Common;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace Provider
{
    public abstract class Device
    {
        public readonly DeviceMetadata DeviceMetadata;

        private readonly SemaphoreSlim deviceConnectionChangesSemaphore = new SemaphoreSlim(1, 1);

        public Device(DeviceMetadata deviceMetadata)
        {
            DeviceMetadata = deviceMetadata;
        }

        public bool IsConnected { get; private set; }
        public bool IsTurnedOn { get; private set; }

        public async Task<Color> GetColor()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.GetColor))
            {
                return await GetColorInternal();
            }

            return Color.Empty;
        }

        protected abstract Task<Color> GetColorInternal();

        public async Task SetColor(Color color)
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.SetColor))
            {
                await SetColorInternal(color);
            }
        }

        protected abstract Task SetColorInternal(Color color);

        public async Task SetColorSmoothly(Color color, int relativeSmoothness)
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.SetColorSmoothly))
            {
                await SetColorSmoothlyInternal(color, relativeSmoothness);
            }
        }

        protected abstract Task SetColorSmoothlyInternal(Color color, int relativeSmoothness);

        public async Task<byte> GetBrightnessPercentage()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.GetBrightness))
            {
               return await GetBrightnessPercentageInternal();
            }

            return 0;
        }

        protected abstract Task<byte> GetBrightnessPercentageInternal();

        public async Task SetBrightnessPercentage(byte brightness)
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.SetBrightness))
            {
                await SetBrightnessPercentageInternal(brightness);
            }
        }

        protected abstract Task SetBrightnessPercentageInternal(byte brightness);

        public async Task TurnOn()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.TurnOn))
            {
                await TurnOnInternal();
                IsTurnedOn = true;
            }
        }

        public async Task TurnOff()
        {
            if (DeviceMetadata.SupportedOperations.Contains(OperationType.TurnOff))
            {
                await TurnOffInternal();
                IsTurnedOn = false;
            }
        }

        protected abstract Task TurnOnInternal();
        protected abstract Task TurnOffInternal();

        public async Task<bool> Connect()
        {
            await deviceConnectionChangesSemaphore.WaitAsync();

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
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Failed connecting to device with GUID {A}.", DeviceMetadata.RgbMasterDeviceGuid);

                    IsConnected = false;
                    didSucceed = false;
                }
            }

            deviceConnectionChangesSemaphore.Release();

            return didSucceed;
        }

        public async Task<bool> Disconnect()
        {
            await deviceConnectionChangesSemaphore.WaitAsync();

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
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Failed disconnecting from device with GUID {A}", DeviceMetadata.RgbMasterDeviceGuid);

                    IsConnected = false;
                    didSucceed = false;
                }
            }

            deviceConnectionChangesSemaphore.Release();

            return didSucceed;
        }

        protected abstract Task ConnectInternal();
        protected abstract Task DisconnectInternal();
    }
}
