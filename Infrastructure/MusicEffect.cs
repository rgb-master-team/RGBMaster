//using NAudio.Wave;
using NAudio.Wave;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MusicEffect : Effect
    {
        private AsioOut captureInstance = null;

        public override Task StartInternal()
        {
            var driverNames = AsioOut.GetDriverNames();

            var asioOut = new AsioOut(driverNames[0]);

            var inputChannels = asioOut.DriverInputChannelCount;

            asioOut.InputChannelOffset = 4;
            var recordChannelCount = 2;
            var sampleRate = 44100;
            asioOut.InitRecordAndPlayback(null, recordChannelCount, sampleRate);

            asioOut.AudioAvailable += AsioOut_AudioAvailable;

            asioOut.Play();

            captureInstance = asioOut;

            return Task.CompletedTask;
        }

        private void AsioOut_AudioAvailable(object sender, AsioAudioAvailableEventArgs e)
        {
            var samples = e.GetAsInterleavedSamples();

            float max = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                var sample = samples[i];

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

            foreach (var device in devices)
            {
                if (device.SupportedOperations.Contains(OperationType.SetBrightness))
                {
                    tasks.Add(Task.Run(() => device.SetBrightnessPercentage((byte)(max))));
                }

                if (device.SupportedOperations.Contains(OperationType.SetColor))
                {
                    tasks.Add(Task.Run(() => device.SetColor(color)));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        public override Task StopInternal()
        {
            captureInstance.Stop();
            captureInstance.Dispose();

            return Task.CompletedTask;
        }

        /*public override Task StartInternal()
        {
            captureInstance = SoundHelper.GetCaptureInstance();

            captureInstance.DataAvailable += (ss, ee) => OnNewSoundReceived(ss, ee, devices);
            captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();

            captureInstance.StartRecording();

            return Task.CompletedTask;
        }*/

        /*private void OnNewSoundReceived(object sender, NAudio.Wave.WaveInEventArgs e, IEnumerable<Device> currDevices)
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
                    tasks.Add(Task.Run(() => device.SetBrightnessPercentage((byte)(max))));
                }

                if (device.SupportedOperations.Contains(OperationType.SetColor))
                {
                    tasks.Add(Task.Run(() => device.SetColor(color)));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }*/
    }
}
