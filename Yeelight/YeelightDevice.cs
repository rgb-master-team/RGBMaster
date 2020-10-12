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

        public YeelightDevice(Guid discoveringProvider, YeelightAPI.Device internalDevice): base(new YeelightDeviceMetadata(discoveringProvider, GetDeviceTypeForYeelight(internalDevice), !String.IsNullOrWhiteSpace(internalDevice.Name) ? internalDevice.Name: internalDevice.Hostname, GetSupportedOperationsForYeelight(internalDevice)))
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

            if (!supportedOps.Contains(YeelightAPI.Models.METHODS.SetMusicMode))
            {
                throw new YeelightDeviceNotSupportedException();
            }

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

        protected override byte GetBrightnessPercentageInternal()
        {
            // TODO - Don't get this from the music socket as it won't return anything to us - 
            // move to standard request like in the API specifications.
            var task = InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.bright);
            task.Wait();
            return (byte)task.Result;
        }

        protected override Color GetColorInternal()
        {
            // TODO - Don't get this from the music socket as it won't return anything to us - 
            // move to standard request like in the API specifications.
            var task = InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.rgb);
            task.Wait();
            return RGBColorHelper.ParseColor((int)task.Result);
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
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

            musicModeSocket.Send(sentData);
        }

        protected override void SetColorInternal(Color color)
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

            musicModeSocket.Send(colorSentData);
        }

        protected override void TurnOffInternal()
        {
            Command turnOffCommand = new Command()
            {
                Id = 1,
                Method = "set_power",
                Params = new List<object>() { "off", "smooth", 500 }
            };

            string turnOffJSON = JsonConvert.SerializeObject(turnOffCommand, DeviceSerializerSettings);
            byte[] turnOffSentData = Encoding.ASCII.GetBytes(turnOffJSON + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

            musicModeSocket.Send(turnOffSentData);
        }

        protected override void TurnOnInternal()
        {
            Command turnOnCommand = new Command()
            {
                Id = 1,
                Method = "set_power",
                Params = new List<object>() { "on", "smooth", 500 }
            };

            string turnOnJSON = JsonConvert.SerializeObject(turnOnCommand, DeviceSerializerSettings);
            byte[] turnOnSentData = Encoding.ASCII.GetBytes(turnOnJSON + "\r\n"); // \r\n is the end of the message, it needs to be sent for the message to be read by the device

            musicModeSocket.Send(turnOnSentData);
        }
    }
}
