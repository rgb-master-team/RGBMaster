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
using Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Networking.Sockets;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsAudioPointsEditingEnabled => !AppState.Instance.IsEffectRunning;

        public List<MusicEffectAudioPoint> AudioPoints
        {
            get
            {
                return ((MusicEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Music)).EffectProperties.AudioPoints;
            }
            set
            {
                var musicEffectProperties = ((MusicEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Music)).EffectProperties;
                musicEffectProperties.AudioPoints = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(AudioPointsCount));
            }
        }

        public readonly List<int> PossibleAudioPointsCount = Enumerable.Range(1, 100).ToList();

        public int AudioPointsCount
        {
            get
            {
                return AudioPoints.Count;
            }
        }

        public MusicEffectControl()
        {
            this.InitializeComponent();
            AppState.Instance.PropertyChanged += AppStateInstance_PropertyChanged;
        }

        private void AppStateInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppState.Instance.IsEffectRunning))
            {
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(IsAudioPointsEditingEnabled));
            }
        }

        private void ColorPickButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var teachingTip = (TeachingTip)button.Resources["AudioPointEditTeachingTip"];
            teachingTip.Target = button;
            teachingTip.IsOpen = true;
        }

        private void AudioPointEditTeachingTip_Closed(TeachingTip sender, TeachingTipClosedEventArgs args)
        {
            AudioPoints = AudioPoints.OrderBy(x => x.MinimumAudioPoint).ToList();
        }

        private void AudioPointsComboBox_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            var newCountText = args.Text;

            if (!int.TryParse(newCountText, out var newCount) || newCount < 1 || newCount > 100)
            {
                var flyoutStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                flyoutStackPanel.Children.Add(new FontIcon
                {
                    FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"),
                    Glyph = "\uE783",
                    FontSize = 42,
                    Margin = new Thickness(0, 0, 8, 0)
                });

                flyoutStackPanel.Children.Add(new TextBlock
                {
                    Text = "The audio points count must be from 1 to 100."
                });

                var flyoutPresenterStyle = new Style(typeof(FlyoutPresenter));
               
                //TODO - Change Property based on window size
                //Get page FrameworkElement to point the flyout at the botoom of the screnn.
                flyoutPresenterStyle.Setters.Add(new Setter(FrameworkElement.MaxWidthProperty, 1000));


                var flyout = new Flyout()
                {
                    Content = flyoutStackPanel,
                    FlyoutPresenterStyle = flyoutPresenterStyle,
                    Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Bottom,
                    XamlRoot = sender.XamlRoot,
                };

                flyout.ShowAt(sender);

                args.Handled = true;
            }
            else
            {
                if (AudioPoints.Count < newCount)
                {
                    for (int i = AudioPoints.Count; i < newCount; i++)
                    {
                        AudioPoints.Add(new MusicEffectAudioPoint() { Index = i, MinimumAudioPoint = (double)i / 100, Color = Color.White });
                    }

                    AudioPoints = new List<MusicEffectAudioPoint>(AudioPoints);
                }
                else if (AudioPoints.Count > newCount)
                {
                    AudioPoints = AudioPoints.GetRange(0, newCount);
                }
            }
        }

        private async void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            await InfoContentDialog.ShowAsync();
        }
    }
}
