﻿using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public List<GradientPoint> GradientPoints
        {
            get
            {
                return ((GradientEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Gradient)).EffectProperties.GradientPoints;
            }
            set
            {
                ((GradientEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.Gradient)).EffectProperties.GradientPoints = value;
            }
        }

        public GradientEffectControl()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            GradientPoints = new List<GradientPoint>()
            {
                new GradientPoint()
                {
                    Color = Color.Red,
                    RelativeSmoothness = 10000
                },
                new GradientPoint()
                {
                    Color = Color.Green,
                    RelativeSmoothness = 10000
                },
                new GradientPoint()
                {
                    Color = Color.Blue,
                    RelativeSmoothness = 10000
                }
            };
        }
    }
}