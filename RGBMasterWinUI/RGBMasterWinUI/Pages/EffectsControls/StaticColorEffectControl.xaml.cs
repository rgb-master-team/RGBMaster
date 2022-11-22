// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI.Pages.EffectsControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StaticColorEffectControl : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string SmoothnessSettingsKey = "StaticColorEffectSmoothness";

        public StaticColorEffectMetadata StaticColorEffectMd => (StaticColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.StaticColor);
        public StaticColorEffectProps StaticColorEffectProps => StaticColorEffectMd.EffectProperties;

        public int RelativeSmoothness
        {
            get
            {
                return StaticColorEffectProps.RelativeSmoothness;
            }
            set
            {
                StaticColorEffectProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public StaticColorEffectControl()
        {
            LoadSettings();

            this.InitializeComponent();

            var lastStateRgbColor = StaticColorEffectProps.SelectedColor;

            ColorPicker.Color = new Windows.UI.Color()
            {
                R = lastStateRgbColor.R,
                G = lastStateRgbColor.G,
                B = lastStateRgbColor.B
            };

            Brighness_Slider.Value = StaticColorEffectProps.SelectedBrightness;
        }

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            var color = System.Drawing.Color.FromArgb(sender.Color.R, sender.Color.G, sender.Color.B);
            EventManager.Instance.ChangeStaticColor(new StaticColorEffectProps() { SelectedColor = color, SelectedBrightness = StaticColorEffectProps.SelectedBrightness, RelativeSmoothness = StaticColorEffectProps.RelativeSmoothness });
        }

        private void Brightness_Value_Changed(object sender, RangeBaseValueChangedEventArgs e)
        {
            EventManager.Instance.ChangeStaticColor(new StaticColorEffectProps() { SelectedColor = StaticColorEffectProps.SelectedColor, SelectedBrightness = (byte)Brighness_Slider.Value, RelativeSmoothness = StaticColorEffectProps.RelativeSmoothness });
        }

        private void LoadSmoothness()
        {
            EventManager.Instance.LoadUserSetting(SmoothnessSettingsKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(SmoothnessSettingsKey, out var smoothnessObj))
            {
                int smoothness = (int)smoothnessObj;
                RelativeSmoothness = smoothness;
            }
            else
            {
                RelativeSmoothness = 0;
            }
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(SmoothnessSettingsKey, RelativeSmoothness));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void EffectControl_ResetButton_Clicked(object sender, EventArgs e)
        {
            EventManager.Instance.ResetUserSettingsToDefault(new List<string>() { SmoothnessSettingsKey });
            LoadSettings();

            ColorPicker.Color = Windows.UI.Color.FromArgb(0, 255, 255, 255);
            Brighness_Slider.Value = 100;
        }

        private void LoadSettings()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);
            LoadSmoothness();
        }
    }
}
