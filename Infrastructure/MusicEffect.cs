using NAudio.Wave;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MusicEffect : IEffect
    {
        private WasapiLoopbackCapture captureInstance = null;

        public Task Start(IEnumerable<Device> devices)
        {
            captureInstance = SoundHelper.GetCaptureInstance();

            captureInstance.DataAvailable += async (ss, ee) => await OnNewSoundReceived(ss, ee, devices);
            captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();

            captureInstance.StartRecording();
            
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            captureInstance.StopRecording();

            return Task.CompletedTask;
        }

        private async Task OnNewSoundReceived(object sender, NAudio.Wave.WaveInEventArgs e, IEnumerable<Device> currDevices)
        {
            float max = 0;
            var buffer = new WaveBuffer(e.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < e.BytesRecorded / 4; index++)
            {
                float sample = buffer.FloatBuffer[index];

                // absolute value 
                //if (sample < 0) sample = -sample;
                if (sample < 0) sample = -sample;
                // is this the max value?
                if (sample > max) max = sample;
            }

            Color color = Color.Black;

            if (max > 0.01 && max <= 0.1)
            {
                max = 1;
                color = Color.Red;
            }

            if (max > 0.1 && max <= 0.2)
            {
                max = 1;
                color = Color.Orange;
            }

            else if (max > 0.2 && max <= 0.35)
            {
                max = 30;
                color = Color.Yellow;
            }

            else if (max > 0.35 && max <= 0.5)
            {
                max = 30;
                color = Color.Cyan;
            }

            else if (max > 0.5 && max <= 0.65)
            {
                max = 60;
                color = Color.Blue;
            }

            else if (max > 0.65)
            {
                max = 100;
                color = Color.Violet;
            }
            else
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var device in currDevices)
            {
                if (device.SupportedOperations.Contains(OperationType.SetBrightness))
                {
                    tasks.Add(device.SetBrightnessPercentage((byte)(max * 100)));
                }

                if (device.SupportedOperations.Contains(OperationType.SetColor))
                {
                    tasks.Add(device.SetColor(color));
                }
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
