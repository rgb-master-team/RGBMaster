using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RGBMasterUWPApp.Pages.EffectsControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GradientEffectControl : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string GradientPointsUserSettingKey = "GradientEffectGradientPoints";
        private const string DelayIntervalUserSettingKey = "GradientEffectDelayInterval";
        private const string RelativeSmoothnessUserSettingKey = "GradientEffectRelativeSmoothness";

        public Thickness AudioStopButtonMargin
        {
            get
            {
                return new Thickness(0, 0, ((1.0 / GradientStops.Count) * GradientStopsBrushRectangle.Width) - 30, 0);
            }
        }

        public GradientEffectMetadataProperties GradientEffectMdProps => ((GradientEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Gradient)).EffectProperties;

        public int DelayInterval
        {
            get
            {
                return GradientEffectMdProps.DelayInterval;
            }
            set
            {
                GradientEffectMdProps.DelayInterval = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public int RelativeSmoothness
        {
            get
            {
                return GradientEffectMdProps.RelativeSmoothness;
            }
            set
            {
                GradientEffectMdProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public List<GradientPoint> GradientPoints
        {
            get
            {
                return GradientEffectMdProps.GradientPoints;
            }
            set
            {
                GradientEffectMdProps.GradientPoints = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(GradientStops));
                ReApplyGradientStopsButtonsStyle();
            }
        }

        private void ReApplyGradientStopsButtonsStyle()
        {
            // HACK
            var newStyle = new Style(typeof(GridViewItem));
            newStyle.Setters.Add(new Setter(GridViewItem.MarginProperty, AudioStopButtonMargin));
            GradientStopButtonsGridView.ItemContainerStyle = newStyle;
        }

        public GradientStopCollection GradientStops
        {
            get
            {
                var gradientPoints = GradientEffectMdProps.GradientPoints;

                var gradientStops = new GradientStopCollection();

                for (int i = 0; i < gradientPoints.Count; i++)
                {
                    var gradientPoint = gradientPoints[i];

                    gradientStops.Add(new GradientStop()
                    {
                        // TODO - USE THE DAMN CONVERTER
                        Color = Windows.UI.Color.FromArgb(gradientPoint.Color.A, gradientPoint.Color.R, gradientPoint.Color.G, gradientPoint.Color.B),
                        Offset = (double)i / gradientPoints.Count
                    });
                }

                return gradientStops;
            }
        }

        public GradientEffectControl()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);
            LoadGradientPoints();
            LoadDelayInterval();
            LoadRelativeSmoothness();

            this.InitializeComponent();
            ReApplyGradientStopsButtonsStyle();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SaveUserSettingsForPage();
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(GradientPointsUserSettingKey, JsonConvert.SerializeObject(GradientPoints)));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(DelayIntervalUserSettingKey, DelayInterval));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(RelativeSmoothnessUserSettingKey, RelativeSmoothness));
        }

        private void LoadRelativeSmoothness()
        {
            EventManager.Instance.LoadUserSetting(RelativeSmoothnessUserSettingKey);
            if (AppState.Instance.UserSettingsCache.TryGetValue(RelativeSmoothnessUserSettingKey, out var relativeSmoothness))
            {
                RelativeSmoothness = (int)relativeSmoothness;
            }
            else
            {
                RelativeSmoothness = 0;
            }
        }

        private void LoadDelayInterval()
        {
            EventManager.Instance.LoadUserSetting(DelayIntervalUserSettingKey);
            if (AppState.Instance.UserSettingsCache.TryGetValue(DelayIntervalUserSettingKey, out var delayInterval))
            {
                DelayInterval = (int)delayInterval;
            }
            else
            {
                DelayInterval = 0;
            }
        }

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void LoadGradientPoints()
        {
            EventManager.Instance.LoadUserSetting(GradientPointsUserSettingKey);
            if (AppState.Instance.UserSettingsCache.TryGetValue(GradientPointsUserSettingKey, out var gradientPointsJsonObject) &&
                !string.IsNullOrWhiteSpace(gradientPointsJsonObject as string))
            {
                var gradientPointsJson = (string)gradientPointsJsonObject;
                try
                {
                    var gradientPoints = JsonConvert.DeserializeObject<List<GradientPoint>>(gradientPointsJson);
                    GradientPoints = gradientPoints;
                }
                catch (Exception ex)
                {
                    // TODO - ADD LOGGING SERVICE!
                }
            }
        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            var flyoutButton = (MenuFlyoutItem)sender;
            var parentButton = (Button)flyoutButton.FindName("GradientStopButton");
            var teachingTip = (TeachingTip)parentButton.Resources["GradientPointTeachingTip"];

            teachingTip.Target = parentButton;
            teachingTip.IsOpen = true;
        }

        private void ColorPicker_ColorChanged(Windows.UI.Xaml.Controls.ColorPicker sender, Windows.UI.Xaml.Controls.ColorChangedEventArgs args)
        {
            if (!sender.IsLoaded)
            {
                return;
            }

            var newColor = System.Drawing.Color.FromArgb(args.NewColor.A, args.NewColor.R, args.NewColor.G, args.NewColor.B);

            ((GradientPoint)sender.DataContext).Color = newColor;
            NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(GradientPoints));
            NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this, nameof(GradientStops));
        }

        private void AddGradientPointAfter_Click(object sender, RoutedEventArgs e)
        {
            var newGradientPoints = new List<GradientPoint>(GradientPoints);

            var flyoutItem = (MenuFlyoutItem)sender;

            var clickedGradientPoint = (GradientPoint)flyoutItem.DataContext;

            var newGradientPointIndex = clickedGradientPoint.Index + 1;

            for (int i = newGradientPointIndex; i < newGradientPoints.Count; i++)
            {
                newGradientPoints[i].Index++;
            }

            newGradientPoints.Insert(newGradientPointIndex, new GradientPoint()
            {
                Color = Color.White,
                Index = newGradientPointIndex
            });

            GradientPoints = newGradientPoints;
        }

        private void AddGradientPointBefore_Click(object sender, RoutedEventArgs e)
        {
            var newGradientPoints = new List<GradientPoint>(GradientPoints);

            var flyoutItem = (MenuFlyoutItem)sender;

            var clickedGradientPoint = (GradientPoint)flyoutItem.DataContext;

            var newGradientPointIndex = clickedGradientPoint.Index;

            for (int i = newGradientPointIndex; i < newGradientPoints.Count; i++)
            {
                newGradientPoints[i].Index++;
            }

            newGradientPoints.Insert(newGradientPointIndex, new GradientPoint()
            {
                Color = Color.White,
                Index = newGradientPointIndex
            });

            GradientPoints = newGradientPoints;
        }

        private void DuplicateGradientPoint_Click(object sender, RoutedEventArgs e)
        {
            var newGradientPoints = new List<GradientPoint>(GradientPoints);

            var flyoutItem = (MenuFlyoutItem)sender;

            var clickedGradientPoint = (GradientPoint)flyoutItem.DataContext;

            var newGradientPointIndex = clickedGradientPoint.Index + 1;

            for (int i = newGradientPointIndex; i < newGradientPoints.Count; i++)
            {
                newGradientPoints[i].Index++;
            }

            newGradientPoints.Insert(newGradientPointIndex, new GradientPoint()
            {
                Color = clickedGradientPoint.Color,
                Index = newGradientPointIndex,
            });

            GradientPoints = newGradientPoints;
        }

        private void RemoveGradientPoint_Click(object sender, RoutedEventArgs e)
        {
            var newGradientPoints = new List<GradientPoint>(GradientPoints);

            var flyoutItem = (MenuFlyoutItem)sender;

            var clickedGradientPoint = (GradientPoint)flyoutItem.DataContext;

            for (int i = clickedGradientPoint.Index + 1; i < newGradientPoints.Count; i++)
            {
                newGradientPoints[i].Index--;
            }

            newGradientPoints.RemoveAt(clickedGradientPoint.Index);

            GradientPoints = newGradientPoints;
        }
    }
}
