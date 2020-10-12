using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RGBMasterUWPApp.Pages.EffectsControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicEffectControl : Page, INotifyPropertyChanged
    {
        private List<MusicEffectAudioPoint> audioPoints;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsAudioPointsEditingEnabled => !AppState.Instance.IsEffectRunning;

        public List<MusicEffectAudioPoint> AudioPoints
        {
            get
            {
                return audioPoints;
            }
            set
            {
                audioPoints = value;
                OnPropertyChanged();

            }
        }

        public readonly List<int> PossibleAudioPointsCount = Enumerable.Range(1, 100).ToList();
        public int AudioPointsCount;


        public MusicEffectControl()
        {
            this.InitializeComponent();
            AppState.Instance.PropertyChanged += AppStateInstance_PropertyChanged;
        }

        private void AppStateInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppState.Instance.IsEffectRunning))
            {
                OnPropertyChanged(nameof(IsAudioPointsEditingEnabled));
            }
        }

        private void AudioPointsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newCount = (int)AudioPointsComboBox.SelectedValue;
            AudioPoints = new List<MusicEffectAudioPoint>(newCount);
            for (int i = 0; i < newCount; i++)
            {
                AudioPoints.Add(new MusicEffectAudioPoint() { Index = i, MinimumAudioPoint = (double)i / 10, Color = Color.White });
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void ColorPickButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var teachingTip = (TeachingTip)button.Resources["AudioPointEditTeachingTip"];
            teachingTip.Target = button;
            teachingTip.IsOpen = true;
        }

        private void ColorPicker_ColorChanged(Windows.UI.Xaml.Controls.ColorPicker sender, Windows.UI.Xaml.Controls.ColorChangedEventArgs args)
        {

        }
    }
}
