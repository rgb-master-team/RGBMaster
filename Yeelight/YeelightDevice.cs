using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Yeelight
{
    public class YeelightDevice : Device
    {
        private readonly YeelightAPI.Device InternalDevice;
        private Socket musicModeSocket;

        public YeelightDevice(YeelightAPI.Device internalDevice)
        {
            InternalDevice = internalDevice;
        }

        public async override Task Connect()
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

                    this.musicModeSocket = musicModeSocketListener.Accept();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to bind the socket to the specified ip and port.", ex);
                }
            }
        }

        public override Task Disconnect()
        {
            InternalDevice.Disconnect();
            return Task.CompletedTask;
        }

        public async override Task<byte> GetBrightnessPercentage()
        {
            // TODO - Also implement background lighting???
            // TODO2 - Keep the last known brightness at all time in a private member? is it a sensible approach?            
            var percentage = await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.bright);
            return (byte)percentage;
        }

        public async override Task<Color> GetColor()
        {
            var hexColor = (int)(await InternalDevice.GetProp(YeelightAPI.Models.PROPERTIES.rgb));
            int r = ((byte)(hexColor >> 16)); // = 0
            int g = ((byte)(hexColor >> 8)); // = 0
            int b = ((byte)(hexColor >> 0)); // = 255
            return Color.FromArgb(r, g, b);
        }

        public async override Task SetBrightnessPercentage(byte brightness)
        {
            await InternalDevice.SetBrightness(brightness);
        }

        public async override Task SetColor(Color color)
        {
            await InternalDevice.SetRGBColor(color.R, color.G, color.B);
        }
    }
}
