using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using chroma_yeelight.Effects.Common;
using chroma_yeelight.Exceptions;
using Infrastructure;
using NAudio.Wave;

namespace chroma_yeelight.Effects.Music
{
	public class MusicEffect : Effect
	{
		private readonly MainWindow _mainWindow;

		private int count1 = 0;
		private int count2 = 0;
		private int count3 = 0;
		private int count4 = 0;
		private WasapiLoopbackCapture captureInstance;

		public override string Name => "Music";

		public MusicEffect(MainWindow window, IEnumerable<Device> devices) : base(devices)
		{
			_mainWindow = window;
		}

		public override void Start()
		{
			this.captureInstance = SoundHelper.GetCaptureInstance();

			captureInstance.DataAvailable += async (ss, ee) => await this.OnNewSoundReceived(ss, ee, _devices);
			captureInstance.RecordingStopped += (ss, ee) => captureInstance.Dispose();

			try
			{
				captureInstance.StartRecording();
			}
			catch (Exception ex)
			{
				throw new AudioCaptureAccessDeniedException(ex);
			}
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
			// ColorHelper.ComputeRGBColor(ColoreColor.Purple.R, ColoreColor.Purple.G, ColoreColor.Purple.B)

			if (max > 0.01 && max <= 0.1)
			{
				max = 1;
				count1++;
				color = Color.Red;
			}

			if (max > 0.1 && max <= 0.2)
			{
				max = 1;
				count1++;
				color = Color.Orange;
			}

			else if (max > 0.2 && max <= 0.35)
			{
				max = 30;
				count2++;
				color = Color.Yellow;
			}

			else if (max > 0.35 && max <= 0.5)
			{
				max = 30;
				count2++;
				color = Color.Cyan;
			}

			else if (max > 0.5 && max <= 0.65)
			{
				max = 60;
				count3++;
				color = Color.Blue;
			}

			else if (max > 0.65)
			{
				max = 100;
				count4++;
				color = Color.Violet;
			}

			bool shouldSyncBrightness = false;
			bool shouldSyncColor = false;

			_mainWindow.Dispatcher.Invoke(() =>
			{
				shouldSyncBrightness = _mainWindow.syncBrightnessChkBox.IsChecked.Value;
				shouldSyncColor = _mainWindow.syncColorChkBox.IsChecked.Value;

				_mainWindow.musicProgressBar.Value = max;
				_mainWindow.musicProgressBar.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(color.R, color.G, color.B));
			});

			var tasks = new List<Task>();

			foreach (var device in currDevices)
			{
				if (shouldSyncBrightness && device.SupportedOperations.Contains(OperationType.SetBrightness))
				{
					tasks.Add(device.SetBrightnessPercentage((byte)(max * 100)));
				}

				if (shouldSyncColor && device.SupportedOperations.Contains(OperationType.SetColor))
				{
					tasks.Add(device.SetColor(color));
				}
			}

			await Task.WhenAll(tasks.ToArray());
		}

		public override void Stop()
		{
			captureInstance.StopRecording();
		}
	}
}
