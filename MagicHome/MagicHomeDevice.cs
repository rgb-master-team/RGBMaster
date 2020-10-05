using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MagicHome
{
    public class MagicHomeDevice : Device
    {
        private const int defaultMagicHomePort = 5577;

        private readonly string LightIp;
        private readonly Socket InternalLightSocket;
        private LedProtocol MagicHomeProtocol;
        private bool shouldUseCsum;

        public MagicHomeDevice(string lightIp, MagicHomeDeviceMetadata magicHomeDeviceMetadata) : base(magicHomeDeviceMetadata)
        {
            LightIp = lightIp;
            InternalLightSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 1000,
                SendTimeout = 1000
            };
        }

        protected override async Task ConnectInternal()
        {
            await InternalLightSocket.ConnectAsync(IPAddress.Parse(LightIp), defaultMagicHomePort);

            MagicHomeProtocol = await GetMagicHomeProtocol();

            switch (MagicHomeProtocol)
            {
                case LedProtocol.Unknown:
                    shouldUseCsum = false;
                    break;
                case LedProtocol.LEDENET:
                    shouldUseCsum = true;
                    break;
                case LedProtocol.LEDENET_ORIGINAL:
                    shouldUseCsum = false;
                    break;
                default:
                    shouldUseCsum = false;
                    break;
            }
        }

        protected override Task DisconnectInternal()
        {
            InternalLightSocket.Close();
            InternalLightSocket.Dispose();

            return Task.CompletedTask;
        }

        protected override byte GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Color GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override void SetColorInternal(System.Drawing.Color color)
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                SendDataToDevice(0x41, color.R, color.G, color.B, 0x00, 0x00, 0x0f).Wait();
            }
            else
            {
                SendDataToDevice(0x56, color.R, color.G, color.B, 0xaa).Wait();
            }
        }

        protected override void TurnOffInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                SendDataToDevice(0x71, 0x24, 0x0f).Wait();
            }
            else
            {
                SendDataToDevice(0xcc, 0x24, 0x33).Wait();
            }
        }

        protected override void TurnOnInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                SendDataToDevice(0x71, 0x23, 0x0f).Wait();
            }
            else
            {
                SendDataToDevice(0xcc, 0x23, 0x33).Wait();
            }
        }

        private async Task<LedProtocol> GetMagicHomeProtocol()
        {
            await SendDataToDevice(0x81, 0x8a, 0x8b);

            try
            {
                byte[] buffer_ledenet = new byte[14];

                await InternalLightSocket.ReceiveAsync(new ArraySegment<byte>(buffer_ledenet), SocketFlags.None);
                return LedProtocol.LEDENET;
            }
            catch (Exception ex)
            {
                // TODO - Log exception
                await SendDataToDevice(0xef, 0x01, 0x77);
                try
                {
                    byte[] buffer_original = new byte[14];
                    await InternalLightSocket.ReceiveAsync(new ArraySegment<byte>(buffer_original), SocketFlags.None);
                    return LedProtocol.LEDENET_ORIGINAL;
                }
                catch (Exception innerEx)
                {
                    // Log this exception, too - it's more important
                    return LedProtocol.Unknown;
                }
            }
        }

        private async Task SendDataToDevice(params byte[] dataToSend)
        {
            List<byte> finalSentData = new List<byte>();
            finalSentData.AddRange(dataToSend);

            if (shouldUseCsum)
            {
                var csum = dataToSend.Aggregate((byte)0, (total, nextByte) => (byte)(total + nextByte));
                csum = (byte)(csum & 0xFF);

                finalSentData.Add(csum);
            }

            await InternalLightSocket.SendAsync(new ArraySegment<byte>(finalSentData.ToArray()), SocketFlags.None);
        }
    }
}
