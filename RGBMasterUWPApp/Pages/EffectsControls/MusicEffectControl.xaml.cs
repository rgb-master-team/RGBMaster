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
        private const double Gamma = 0.80;
        private const double IntensityMax = 255;

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
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(IsAddAudioPointEnabled));
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(IsRemoveAudioPointEnabled));
            }
        }

        public readonly List<int> PossibleAudioPointsCount = Enumerable.Range(1, 100).ToList();

        public int AudioPointsCount => AudioPoints.Count;

        public bool IsAddAudioPointEnabled => AudioPointsCount < 100;

        public bool IsRemoveAudioPointEnabled => AudioPoints.Count > 1;

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
            //AudioPoints = AudioPoints.OrderBy(x => x.MinimumAudioPoint).ToList();
        }

        private static Color ColorFromWaveLength(double estimatedWavelength)
        {
            double factor;

            double red;
            double green;
            double blue;

            if ((estimatedWavelength >= 380) && (estimatedWavelength < 440))
            {
                red = -(estimatedWavelength - 440) / (440 - 380);
                green = 0.0;
                blue = 1.0;
            }
            else if ((estimatedWavelength >= 440) && (estimatedWavelength < 490))
            {
                red = 0.0;
                green = (estimatedWavelength - 440) / (490 - 440);
                blue = 1.0;
            }
            else if ((estimatedWavelength >= 490) && (estimatedWavelength < 510))
            {
                red = 0.0;
                green = 1.0;
                blue = -(estimatedWavelength - 510) / (510 - 490);
            }
            else if ((estimatedWavelength >= 510) && (estimatedWavelength < 580))
            {
                red = (estimatedWavelength - 510) / (580 - 510);
                green = 1.0;
                blue = 0.0;
            }
            else if ((estimatedWavelength >= 580) && (estimatedWavelength < 645))
            {
                red = 1.0;
                green = -(estimatedWavelength - 645) / (645 - 580);
                blue = 0.0;
            }
            else if ((estimatedWavelength >= 645) && (estimatedWavelength < 781))
            {
                red = 1.0;
                green = 0.0;
                blue = 0.0;
            }
            else
            {
                red = 0.0;
                green = 0.0;
                blue = 0.0;
            }

            // Let the intensity fall off near the vision limits

            if ((estimatedWavelength >= 380) && (estimatedWavelength < 420))
            {
                factor = 0.3 + 0.7 * (estimatedWavelength - 380) / (420 - 380);
            }
            else if ((estimatedWavelength >= 420) && (estimatedWavelength < 701))
            {
                factor = 1.0;
            }
            else if ((estimatedWavelength >= 701) && (estimatedWavelength < 781))
            {
                factor = 0.3 + 0.7 * (780 - estimatedWavelength) / (780 - 700);
            }
            else
            {
                factor = 0.0;
            }

            // Don't want 0^x = 1 for x <> 0
            int redColorComponent = red == 0.0 ? 0 : (int)Math.Round(IntensityMax * Math.Pow(red * factor, Gamma));
            int greenColorComponent = green == 0.0 ? 0 : (int)Math.Round(IntensityMax * Math.Pow(green * factor, Gamma));
            int blueColorComponent = blue == 0.0 ? 0 : (int)Math.Round(IntensityMax * Math.Pow(blue * factor, Gamma));

            return Color.FromArgb(redColorComponent, greenColorComponent, blueColorComponent);
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
                var newAudioPoints = new List<MusicEffectAudioPoint>();

                for (int i = 0; i < newCount; i++)
                {
                    double relativeWavelength = ((double)i / newCount) * (780 - 380) + 380;
                    double relativeAudioPercentage = ((double)i / newCount) * 100;
                    newAudioPoints.Add(new MusicEffectAudioPoint() { Index = i, MinimumAudioPoint = Math.Round(relativeAudioPercentage, MidpointRounding.AwayFromZero), Color = ColorFromWaveLength(relativeWavelength) });
                }

                AudioPoints = newAudioPoints;
            }
        }

        private async void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            await InfoContentDialog.ShowAsync();
        }

        private void RemoveAudioPoint_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyout = (MenuFlyoutItem)sender;
            var audioPoint = (MusicEffectAudioPoint)menuFlyout.DataContext;

            AudioPoints.RemoveAt(audioPoint.Index);

            for (int i = audioPoint.Index; i < AudioPointsCount; i++)
            {
                AudioPoints[i].Index--;
            }

            AudioPoints = new List<MusicEffectAudioPoint>(AudioPoints);
        }

        private void AddAudioPointBefore_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyout = (MenuFlyoutItem)sender;
            var audioPoint = (MusicEffectAudioPoint)menuFlyout.DataContext;

            var newAudioPoint = new MusicEffectAudioPoint() { Index = audioPoint.Index - 1, MinimumAudioPoint = 100, Color = Color.White };

            AudioPoints.Insert(newAudioPoint.Index, newAudioPoint);

            var currentAudioPointsCount = AudioPointsCount;

            for (int i = newAudioPoint.Index + 1; i < currentAudioPointsCount; i++)
            {
                AudioPoints[i].Index++;
            }

            AudioPoints = new List<MusicEffectAudioPoint>(AudioPoints);
        }

        private void AddAudioPointAfter_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyout = (MenuFlyoutItem)sender;
            var audioPoint = (MusicEffectAudioPoint)menuFlyout.DataContext;

            var newAudioPoint = new MusicEffectAudioPoint() { Index = audioPoint.Index + 1, MinimumAudioPoint = 100, Color = Color.White };

            AudioPoints.Insert(newAudioPoint.Index, newAudioPoint);

            var currentAudioPointsCount = AudioPointsCount;

            for (int i = newAudioPoint.Index + 1; i < currentAudioPointsCount; i++)
            {
                AudioPoints[i].Index++;
            }

            AudioPoints = new List<MusicEffectAudioPoint>(AudioPoints);
        }

        private void DuplicateAudioPoint_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyout = (MenuFlyoutItem)sender;
            var audioPoint = (MusicEffectAudioPoint)menuFlyout.DataContext;

            var newAudioPoint = new MusicEffectAudioPoint() { Index = audioPoint.Index + 1, MinimumAudioPoint = audioPoint.MinimumAudioPoint, Color = audioPoint.Color };

            AudioPoints.Insert(newAudioPoint.Index, newAudioPoint);

            var currentAudioPointsCount = AudioPointsCount;

            for (int i = newAudioPoint.Index + 1; i < currentAudioPointsCount; i++)
            {
                AudioPoints[i].Index++;
            }

            AudioPoints = new List<MusicEffectAudioPoint>(AudioPoints);
        }
    }
}
