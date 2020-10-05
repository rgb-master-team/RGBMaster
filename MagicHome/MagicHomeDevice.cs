using Common;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace MagicHome
{
    public class MagicHomeDevice : Device
    {
        private const int defaultMagicHomePort = 5577;

        private readonly string LightIp;

        private Socket InternalLightSocket;
        private LedProtocol MagicHomeProtocol;
        private bool shouldUseCsum;

        public MagicHomeDevice(string lightIp, MagicHomeDeviceMetadata magicHomeDeviceMetadata) : base(magicHomeDeviceMetadata)
        {
            LightIp = lightIp;
            shouldUseCsum = true;
        }

        protected override async Task ConnectInternal()
        {
            InternalLightSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 1000,
                SendTimeout = 1000
            };

            await InternalLightSocket.ConnectAsync(IPAddress.Parse(LightIp), defaultMagicHomePort);

            MagicHomeProtocol = await GetMagicHomeProtocol();

            shouldUseCsum = MagicHomeProtocol == LedProtocol.LEDENET;
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
                TrySendDataToDevice(0x41, color.R, color.G, color.B, 0x00, 0x00, 0x0f).Wait();
            }
            else
            {
                TrySendDataToDevice(0x56, color.R, color.G, color.B, 0xaa).Wait();
            }
        }

        protected override void TurnOffInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                TrySendDataToDevice(0x71, 0x24, 0x0f).Wait();
            }
            else
            {
                TrySendDataToDevice(0xcc, 0x24, 0x33).Wait();
            }
        }

        protected override void TurnOnInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                TrySendDataToDevice(0x71, 0x23, 0x0f).Wait();
            }
            else
            {
                TrySendDataToDevice(0xcc, 0x23, 0x33).Wait();
            }
        }

        private async Task<LedProtocol> GetMagicHomeProtocol()
        {
            var sentSuccessfully = await TrySendDataToDevice(0x81, 0x8a, 0x8b);

            if (!sentSuccessfully)
            {
                throw new TimeoutException("The request for magic home protocol of LEDENET received a timeout");
            }

            var lednetReceiveAttempt = await TryReceiveData(TimeSpan.FromSeconds(1));

            if (lednetReceiveAttempt.Item1)
            {
                return LedProtocol.LEDENET;
            }

            sentSuccessfully = await TrySendDataToDevice(0xef, 0x01, 0x77);

            if (!sentSuccessfully)
            {
                throw new TimeoutException("The request for magic home protocol of LEDENET_ORIGINAL received a timeout");
            }

            var lednetOriginalReceiveAttempt = await TryReceiveData(TimeSpan.FromSeconds(1));

            if (lednetOriginalReceiveAttempt.Item1)
            {
                return LedProtocol.LEDENET_ORIGINAL;
            }

            return LedProtocol.Unknown;
        }

        private async Task<Tuple<bool, byte[]>> TryReceiveData(TimeSpan? timeout = null, int attemptsCount = 1)
        {
            bool didSucceed = false;
            byte[] buffer = new byte[14];

            while (attemptsCount > 0)
            {
                try
                {
                    await InternalLightSocket.ReceiveAsync(
                        buffer, 
                        SocketFlags.None,
                        timeout != null ? new CancellationTokenSource(timeout.Value).Token : default
                        );

                    didSucceed = true;
                    break;
                }
                catch (TaskCanceledException)
                {
                    attemptsCount -= 1;
                }
            }

            if (!didSucceed)
            {
                buffer = null;
            }

            return new Tuple<bool, byte[]>(didSucceed, buffer);
        }

        private async Task<bool> TrySendDataToDevice(params byte[] dataToSend)
        {
            return await TrySendDataToDevice(null, 1, dataToSend.ToArray());
        }

        private async Task<bool> TrySendDataToDevice(TimeSpan timeout, byte[] dataToSend)
        {
            return await TrySendDataToDevice(timeout, 1, dataToSend.ToArray());
        }

        private async Task<bool> TrySendDataToDevice(int attemptsCount, byte[] dataToSend)
        {
            return await TrySendDataToDevice(null, attemptsCount, dataToSend.ToArray());
        }

        private async Task<bool> TrySendDataToDevice(TimeSpan? timeout, int attemptsCount, byte[] dataToSend)
        {
            List<byte> finalSentData = new List<byte>(dataToSend);

            if (shouldUseCsum)
            {
                var csum = dataToSend.Aggregate((byte)0, (total, nextByte) => (byte)(total + nextByte));
                csum = (byte)(csum & 0xFF);

                finalSentData.Add(csum);
            }

            bool didSucceed = false;

            while (attemptsCount > 0)
            {
                try
                {
                    await InternalLightSocket.SendAsync(
                        finalSentData.ToArray(),
                        SocketFlags.None,
                        timeout != null ? new CancellationTokenSource(timeout.Value).Token : default
                        );
                    didSucceed = true;
                    break;
                }
                catch (TaskCanceledException)
                {
                    attemptsCount -= 1;
                }
            }

            return didSucceed;
        }
    }
}
