using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml.Controls;
using RGBMasterUWPApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class GradientEffectControl : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Thickness AudioStopButtonMargin
        {
            get
            {
                return new Thickness(0, 0, ((1.0 / GradientStops.Count) * GradientStopsBrushRectangle.Width)-30, 0);
            }
        }

        public GradientEffectMetadataProperties GradientEffectMdProps => ((GradientEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Gradient)).EffectProperties;

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
            }
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
            this.InitializeComponent();

            // HACK
            var newStyle = new Style(typeof(GridViewItem));
            newStyle.Setters.Add(new Setter(GridViewItem.MarginProperty, AudioStopButtonMargin));
            GradientStopButtonsGridView.ItemContainerStyle = newStyle;
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
                Index = newGradientPointIndex,
                DelayInterval = 100,
                RelativeSmoothness = 300
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
                Index = newGradientPointIndex,
                DelayInterval = 100,
                RelativeSmoothness = 300
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
                DelayInterval = clickedGradientPoint.DelayInterval,
                RelativeSmoothness = clickedGradientPoint.RelativeSmoothness
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
