using AppExecutionManager.State;
using Common;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class MusicEffectExecutor : EffectExecutor
    {
        private IWaveIn captureInstance = null;

        private List<MusicEffectAudioPoint> orderedAudioPoints;

        public MusicEffectExecutor() : base(new MusicEffectMetadata())
        {

        }

        protected override Task StopInternal()
        {
            captureInstance.StopRecording();

            captureInstance.Dispose();

            return Task.CompletedTask;
        }

        protected override Task StartInternal()
        {
            var musicEffectMetadataProperties = ((MusicEffectMetadata)executedEffectMetadata).EffectProperties;

            orderedAudioPoints = musicEffectMetadataProperties.AudioPoints.OrderBy(audioPoint => audioPoint.MinimumAudioPoint).ToList();

            var enumerator = new MMDeviceEnumerator();
            var nAudioDevice = enumerator.GetDevice(musicEffectMetadataProperties.CaptureDevice.Id);

            if (musicEffectMetadataProperties.CaptureDevice.FlowType == AudioCaptureDeviceFlowType.Output)
            {
                captureInstance = new WasapiLoopbackCapture(nAudioDevice);
            }
            else if (musicEffectMetadataProperties.CaptureDevice.FlowType == AudioCaptureDeviceFlowType.Input)
            {
                for (int i = 0; i < WaveIn.DeviceCount; i++)
                {
                    var capabilities = WaveIn.GetCapabilities(i);

                    // HACK - This hack was made since when working on capturing input devices
                    // using NAudio's wrappers for windows APIs we seek a WaveIn device.
                    // However, we don't have a way of enumerating those devices completely like in 
                    // output devices or getting a device by an id. So instead, we use the count of input
                    // devices detected on this system, and then call `WaveIn.GetCapabilities(i)` for
                    // every device and then compare names (because we don't have GUIDs or anything else).
                    // Eventually - even the name isn't shown completely - but rather the first 32 chars.
                    // So we make this comparison.
                    if (nAudioDevice.FriendlyName.StartsWith(capabilities.ProductName))
                    {
                        captureInstance = new WaveIn()
                        {
                            DeviceNumber = i
                        };

                        break;
                    }    
                }
            }

            captureInstance.DataAvailable += (ss, ee) => OnNewSoundReceived(ss, ee);
            captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();
            captureInstance.StartRecording();

            return Task.CompletedTask;
        }

        private void OnNewSoundReceived(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            float max = 0;
            var buffer = new WaveBuffer(e.Buffer);
            // interpret as 32 bit floating point audio
            for (int index = 0; index < e.BytesRecorded / 4; index++)
            {
                float sample = buffer.FloatBuffer[index];

                if (sample < 0)
                {
                    sample = -sample;
                }

                if (sample > max)
                {
                    max = sample;
                }
            }

            Color color = Color.Black;

            double maxAudioPoint = max * 100;
            byte desiredBrightnessPercentage = 0;

            // We scan the audio points of the effect properties (assuming they are kept ordered in our state, which
            // is probably a bad thing, we'll think about it later). The first audio point which minimum is surpassed by the maximum
            // level of played audio will represent the desired brightness and color of the sound.
            for (int i = orderedAudioPoints.Count - 1; i >= 0; i--)
            {
                var audioPoint = orderedAudioPoints[i];
                if (maxAudioPoint >= audioPoint.MinimumAudioPoint)
                {
                    desiredBrightnessPercentage = (byte)audioPoint.MinimumAudioPoint;
                    color = audioPoint.Color;
                    break;
                }
            }

            var tasks = new List<Task>();

            foreach (var device in Devices)
            {
                if (device.DeviceMetadata.SupportedOperations.Contains(OperationType.SetBrightness))
                {
                    tasks.Add(Task.Run(() => device.SetBrightnessPercentage(desiredBrightnessPercentage)));
                }

                if (device.DeviceMetadata.SupportedOperations.Contains(OperationType.SetColor))
                {
                    tasks.Add(Task.Run(() => device.SetColor(color)));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
