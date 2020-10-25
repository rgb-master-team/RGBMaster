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

            await InternalLightSocket.ConnectAsync(IPAddress.Parse(LightIp), defaultMagicHomePort).ConfigureAwait(false);

            MagicHomeProtocol = await GetMagicHomeProtocol().ConfigureAwait(false);

            shouldUseCsum = MagicHomeProtocol == LedProtocol.LEDENET;
        }

        protected override Task DisconnectInternal()
        {
            InternalLightSocket.Close();
            InternalLightSocket.Dispose();

            return Task.CompletedTask;
        }

        protected override Task<byte> GetBrightnessPercentageInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task<System.Drawing.Color> GetColorInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task SetBrightnessPercentageInternal(byte brightness)
        {
            throw new NotImplementedException();
        }

        protected override async Task SetColorInternal(System.Drawing.Color color)
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                await TrySendDataToDevice(0x41, color.R, color.G, color.B, 0x00, 0x00, 0x0f).ConfigureAwait(false);
            }
            else
            {
                await TrySendDataToDevice(0x56, color.R, color.G, color.B, 0xaa).ConfigureAwait(false);
            }
        }

        protected override async Task SetGradientInternal(GradientPoint gradientPoint)
        {
            List<byte> data = new List<byte>() { 0x51, gradientPoint.Color.R, gradientPoint.Color.G, gradientPoint.Color.B };

            for (int i = 0; i < 16 - 1; i++)
            {
                data.AddRange(new byte[] { 0, 1, 2, 3 });
            }

            data.AddRange(new byte[] { 0x00, SpeedToDelay(gradientPoint.RelativeSmoothness), Convert.ToByte(0x3a), 0xff, 0x0f });

            byte[] dataReady = data.ToArray();
            await TrySendDataToDevice(dataReady);
        }

        private byte SpeedToDelay(int speed)
        {
            // Speed 31, is approximately 2 second, the fastest option possible. We consider that 100%.
            // Speed 1, is approximately 60 seconds, the slowest option possible. We consider that 1%.
            // These are our boundries.
            // We base our calculation according to these assumptions.
            // We consider every speed point ~2 seconds (just a bit longer maybe).

            int boundSpeed;

            if (speed < 2000)
            {
                boundSpeed = 2000;
            }
            else if (speed > 62000)
            {
                boundSpeed = 62000;
            }
            else
            {
                boundSpeed = speed;
            }

            var estimatedSpeedPoints = 31 - (int)Math.Round(boundSpeed / 2000.0, MidpointRounding.AwayFromZero);
            var estimatedSpeedPercentage = (estimatedSpeedPoints / 31.0) * 100;

            var invertedSpeedPercentage = 100 - estimatedSpeedPercentage;

            byte delay = Convert.ToByte((invertedSpeedPercentage * (0x1f - 1)) / 100);
            delay += 1;

            return delay;
        }

        protected override async Task TurnOffInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                await TrySendDataToDevice(0x71, 0x24, 0x0f).ConfigureAwait(false);
            }
            else
            {
                await TrySendDataToDevice(0xcc, 0x24, 0x33).ConfigureAwait(false);
            }
        }

        protected override async Task TurnOnInternal()
        {
            if (MagicHomeProtocol == LedProtocol.LEDENET)
            {
                await TrySendDataToDevice(0x71, 0x23, 0x0f).ConfigureAwait(false);
            }
            else
            {
                await TrySendDataToDevice(0xcc, 0x23, 0x33).ConfigureAwait(false);
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
