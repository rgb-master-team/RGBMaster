using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Utils;
using YeelightAPI.Models;

namespace Yeelight
{
    public class YeelightDevice : Provider.Device
    {
        /// <summary>
        /// Serializer settings
        /// </summary>
        public static readonly JsonSerializerSettings DeviceSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly YeelightAPI.Device InternalDevice;
        private Socket musicModeSocket;

        public YeelightDevice(Guid discoveringProvider, YeelightAPI.Device internalDevice) : base(new YeelightDeviceMetadata(discoveringProvider, GetDeviceTypeForYeelight(internalDevice), !String.IsNullOrWhiteSpace(internalDevice.Name) ? internalDevice.Name : internalDevice.Hostname, GetSupportedOperationsForYeelight(internalDevice)))
        {
            InternalDevice = internalDevice;
        }

        private static HashSet<OperationType> GetSupportedOperationsForYeelight(YeelightAPI.Device internalDevice)
        {
            var operationTypes = new HashSet<OperationType>();

            foreach (var supportedOp in internalDevice.SupportedOperations)
            {
                switch (supportedOp)
                {
                    case METHODS.SetRGBColor:
                        operationTypes.Add(OperationType.SetColor);
                        operationTypes.Add(OperationType.SetGradient);
                        break;
                    case METHODS.SetBrightness:
                        operationTypes.Add(OperationType.SetBrightness);
                        break;
                    case METHODS.SetPower:
                        operationTypes.Add(OperationType.TurnOn);
                        operationTypes.Add(OperationType.TurnOff);
                        break;
                    default:
                        break;
                }
            }

            return operationTypes;
        }

        private static DeviceType GetDeviceTypeForYeelight(YeelightAPI.Device internalDevice)
        {
            switch (internalDevice.Model)
            {
                case MODEL.Unknown:
                    return DeviceType.Unknown;
                case MODEL.Mono:
                case MODEL.Color:
                case MODEL.Ceiling:
                case MODEL.BedsideLamp:
                case MODEL.DeskLamp:
                case MODEL.TunableWhiteBulb:
                    return DeviceType.Lightbulb;
                case MODEL.Stripe:
                    return DeviceType.LedStrip;
                default:
                    return DeviceType.Unknown;
            }
        }

        protected async override Task ConnectInternal()
        {
            await InternalDevice.Connect();
            var supportedOps = InternalDevice.SupportedOperations;

            if (IsMusicModeSupported())
            {
                await ConnectViaMusicMode().ConfigureAwait(false);
            }
            else
            {
                await ConnectViaApi().ConfigureAwait(false);
            }
        }

        private async Task ConnectViaApi()
        {
            await InternalDevice.Connect().ConfigureAwait(false);
        }

        private async Task ConnectViaMusicMode()
        {
            var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(addr => addr.AddressFamily == AddressFamily.InterNetwork).First();

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 0);


            using (var musicModeSocketListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    musicModeSocketListener.Bind(localEndPoint);

                    musicModeSocketListener.Listen(1);

                    await InternalDevice.StartMusicMode(ipAddress.ToString(), ((IPEndPoint)musicModeSocketListener.LocalEndPoint).Port);

                    musicModeSocket = await musicModeSocketListener.AcceptAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to bind the socket to the specified ip and port.", ex);
                }
            }
        }

        protected override Task DisconnectInternal()
        {
            musicModeSocket?.Close();
            InternalDevice?.Disconnect();
            return Task.CompletedTask;
        }

        protected override async Task<byte> GetBrightnessPercentageInternal()
        {
            // TODO - Don't get this from the music socket as it won't return anything to us - 
            // move to standard request like in the API specifications.
            var result = await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.bright).ConfigureAwait(false);
            return (byte)result;
        }

        protected override async Task<Color> GetColorInternal()
        {
            // TODO - Don't get this from the music socket as it won't return anything to us - 
            // move to standard request like in the API specifications.
            var result = await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.rgb).ConfigureAwait(false);
            return RGBColorHelper.ParseColor((int)result);
        }

        protected override async Task SetBrightnessPercentageInternal(byte brightness)
        {
            if (IsMusicModeSupported())
            {
                var serverParams = new List<object>() { brightness };

                // We create 2 commands that opposite each other.
                // When one is extremely bright, the other one is extremely dark.
                Command brightnessCommand = new Command()
                {
                    Id = 1,
                    Method = "set_bright",
                    Params = serverParams
                };

                string data = JsonConvert.SerializeObject(brightnessCommand, DeviceSerializerSettings);
                byte[] sentData = Encoding.ASCII.GetBytes(data + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                await musicModeSocket.SendAsync(sentData, SocketFlags.None).ConfigureAwait(false);
            }
            else
            {
                await InternalDevice.SetBrightness(brightness).ConfigureAwait(false);
            }
        }

        protected override async Task SetColorInternal(Color color)
        {
            if (IsMusicModeSupported())
            {
                var colorValue = RGBColorHelper.ComputeRGBColor(color.R, color.G, color.B);

                Command colorCommand = new Command()
                {
                    Id = 1,
                    Method = "set_rgb",
                    Params = new List<object>() { colorValue, "smooth", 30 }
                };

                string colorData = JsonConvert.SerializeObject(colorCommand, DeviceSerializerSettings);
                byte[] colorSentData = Encoding.ASCII.GetBytes(colorData + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                await musicModeSocket.SendAsync(colorSentData, SocketFlags.None).ConfigureAwait(false);
            }
            else
            {
                await InternalDevice.SetRGBColor(color.R, color.G, color.B, 30).ConfigureAwait(false);
            }
        }

        protected override async Task TurnOffInternal()
        {
            if (IsMusicModeSupported())
            {
                Command turnOffCommand = new Command()
                {
                    Id = 1,
                    Method = "set_power",
                    Params = new List<object>() { "off", "smooth", 500 }
                };

                string turnOffJSON = JsonConvert.SerializeObject(turnOffCommand, DeviceSerializerSettings);
                byte[] turnOffSentData = Encoding.ASCII.GetBytes(turnOffJSON + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                await musicModeSocket.SendAsync(turnOffSentData, SocketFlags.None).ConfigureAwait(false);

                // HACK - this is done because of the way Yeelight operates...
                await DisconnectInternal();
                await ConnectInternal();
            }
            else
            {
                await InternalDevice.TurnOff(500).ConfigureAwait(false);
            }
        }

        protected override async Task TurnOnInternal()
        {
            if (IsMusicModeSupported())
            {
                Command turnOnCommand = new Command()
                {
                    Id = 1,
                    Method = "set_power",
                    Params = new List<object>() { "on", "smooth", 500 }
                };

                string turnOnJSON = JsonConvert.SerializeObject(turnOnCommand, DeviceSerializerSettings);
                byte[] turnOnSentData = Encoding.ASCII.GetBytes(turnOnJSON + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                await musicModeSocket.SendAsync(turnOnSentData, SocketFlags.None).ConfigureAwait(false);
            }
            else
            {
                await InternalDevice.TurnOn(500).ConfigureAwait(false);
            }
        }

        protected override async Task SetGradientInternal(GradientPoint gradientPoint, int relativeSmoothness)
        {
            if (IsMusicModeSupported())
            {
                var colorValue = RGBColorHelper.ComputeRGBColor(gradientPoint.Color.R, gradientPoint.Color.G, gradientPoint.Color.B);

                Command colorCommand = new Command()
                {
                    Id = 1,
                    Method = "set_rgb",
                    Params = new List<object>() { colorValue, "smooth", relativeSmoothness }
                };

                string colorData = JsonConvert.SerializeObject(colorCommand, DeviceSerializerSettings);
                byte[] colorSentData = Encoding.ASCII.GetBytes(colorData + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

                await musicModeSocket.SendAsync(colorSentData, SocketFlags.None).ConfigureAwait(false);
            }
            else
            {
                await InternalDevice.SetRGBColor(gradientPoint.Color.R, gradientPoint.Color.G, gradientPoint.Color.B, relativeSmoothness).ConfigureAwait(false);
            }
        }

        private bool IsMusicModeSupported()
        {
            return InternalDevice.SupportedOperations.Contains(METHODS.SetMusicMode);
        }
    }
}
