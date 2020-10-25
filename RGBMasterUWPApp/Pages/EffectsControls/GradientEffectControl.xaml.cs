using AppExecutionManager.State;
using Common;
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
    public sealed partial class GradientEffectControl : Page
    {
        public Thickness AudioStopButtonMargin
        {
            get
            {
                return new Thickness(0, 0, (1.0 / GradientStops.Count) * GradientStopsBrushRectangle.Width, 0);
            }
        }

        public GradientEffectMetadataProperties GradientEffectMdProps => ((GradientEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Gradient)).EffectProperties;

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
                        Offset = (double)(i + 1) / gradientPoints.Count
                    });
                }

                return gradientStops;
            }
        }

        public GradientEffectControl()
        {
            this.InitializeComponent();

            // TODO - FIX ALL TODOS DEAN
            GradientEffectMdProps.GradientPoints = new List<GradientPoint>()
            {
                new GradientPoint()
                {
                    Index = 0,
                    Color = Color.Red,
                    RelativeSmoothness = 10000
                },
                new GradientPoint()
                {
                    Index = 1,
                    Color = Color.Green,
                    RelativeSmoothness = 10000
                },
                new GradientPoint()
                {
                    Index = 2,
                    Color = Color.Blue,
                    RelativeSmoothness = 10000
                }
            };

            // HACK
            var newStyle = new Style(typeof(GridViewItem));
            newStyle.Setters.Add(new Setter(GridViewItem.MarginProperty, AudioStopButtonMargin));
            GradientStopButtonsGridView.ItemContainerStyle = newStyle;
        }
    }
}
