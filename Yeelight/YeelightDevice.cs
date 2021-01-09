using Common;
using ComposableAsync;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Provider;
using RateLimiter;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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

        private readonly TimeLimiter timeConstraint = TimeLimiter.GetFromMaxCountByInterval(1, TimeSpan.FromSeconds(1));

        public YeelightDevice(Guid discoveringProvider, YeelightAPI.Device internalDevice) : base(new YeelightDeviceMetadata(
            discoveringProvider, 
            GetDeviceTypeForYeelight(internalDevice), 
            !String.IsNullOrWhiteSpace(internalDevice.Name) ? internalDevice.Name : internalDevice.Hostname, 
            GetSupportedOperationsForYeelight(internalDevice),
            new DeviceInterface() { DeviceInterfaceType = DeviceInterfaceType.Network, DeviceUniqueIdentifier = internalDevice.Id }))
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
                        operationTypes.Add(OperationType.SetColorSmoothly);
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
                await ExecuteIfQuotaAllowsAsync(async () =>
                {
                    await ConnectViaApi().ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
        }

        private async Task ConnectViaApi()
        {
            await InternalDevice.Connect().ConfigureAwait(false);
        }

        private async Task ConnectViaMusicMode()
        {
            var yeelightDeviceIpAddr = IPAddress.Parse(InternalDevice.Hostname);
            var yeelightDevicePort = InternalDevice.Port;

            IPAddress localIpAddress = null;

            var yeelightEndpoint = new IPEndPoint(yeelightDeviceIpAddr, yeelightDevicePort);

            Socket testSocket = new Socket(yeelightEndpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            testSocket.Connect(yeelightEndpoint);

            if (testSocket.LocalEndPoint is IPEndPoint localIpEp)
            {
                localIpAddress = localIpEp.Address;
            }

            testSocket.Close();
            testSocket.Dispose();

            if (localIpAddress == null)
            {
                // TODO - MAKE SOME SPECIAL EXCEPTION FOR THIS CASE WITH FLOWERS AND DECORATIONS
                throw new Exception($"Could not find a matching network adapter of which to connect to yeelight with ip address {yeelightDeviceIpAddr}:{yeelightDevicePort}.");
            }

            //var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(addr => addr.AddressFamily == AddressFamily.InterNetwork).First();

            IPEndPoint localEndPoint = new IPEndPoint(localIpAddress, 0);

            using (var musicModeSocketListener = new Socket(localIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    musicModeSocketListener.Bind(localEndPoint);

                    musicModeSocketListener.Listen(1);

                    var port = ((IPEndPoint)musicModeSocketListener.LocalEndPoint).Port;

                    // Instead of using YeelightAPI's StartMusicMode we do it manually via direct communication with the device.
                    // This is because we want to control the acceptance of the request by the device, so we can randomize the port, own it
                    // and tell the device to connect to it at once.
                    // Otherwise, StartMusicMode tries to bind and listen to requests by itself - and the port we randomly received gets used twice.
                    // ¯\_(ツ)_/¯
                    //await InternalDevice.StartMusicMode(localIpAddress.ToString(), ((IPEndPoint)musicModeSocketListener.LocalEndPoint).Port);

                    InternalDevice.ExecuteCommand(METHODS.SetMusicMode, new List<object>() { 1, localIpAddress.ToString(), port });

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
            InternalDevice.ExecuteCommand(METHODS.SetMusicMode, new List<object>() { 0 });
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
                await ExecuteIfQuotaAllowsAsync(async () =>
                {
                    await InternalDevice.SetBrightness(brightness).ConfigureAwait(false);
                }).ConfigureAwait(false);
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
                await ExecuteIfQuotaAllowsAsync(async () =>
                {
                    await InternalDevice.SetRGBColor(color.R, color.G, color.B, 30).ConfigureAwait(false);
                }).ConfigureAwait(false);
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
                await ExecuteIfQuotaAllowsAsync(async () =>
                {
                    await InternalDevice.TurnOff(500).ConfigureAwait(false);
                }).ConfigureAwait(false);
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

        protected override async Task SetColorSmoothlyInternal(Color color, int relativeSmoothness)
        {
            if (IsMusicModeSupported())
            {
                var colorValue = RGBColorHelper.ComputeRGBColor(color.R, color.G, color.B);

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
                await ExecuteIfQuotaAllowsAsync(async () =>
                {
                    await InternalDevice.SetRGBColor(color.R, color.G, color.B, relativeSmoothness).ConfigureAwait(false);
                });
            }
        }

        private bool IsMusicModeSupported()
        {
            return InternalDevice.SupportedOperations.Contains(METHODS.SetMusicMode);
        }

        private async Task ExecuteIfQuotaAllowsAsync(Action ac, int timeoutInMs = 2000, [CallerMemberName] string callingFunction = "")
        {
            await timeConstraint.Enqueue(ac, new CancellationTokenSource(timeoutInMs).Token).ContinueWith(continuation =>
            {
                if (continuation.Status == TaskStatus.Canceled)
                {
                    Log.Logger.Verbose($"Method {callingFunction} fell off the rate limit.");
                }
            }).ConfigureAwait(false);
        }
    }
}
