using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public sealed partial class StaticColorEffectControl : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string SmoothnessSettingsKey = "StaticColorEffectSmoothness";

        public StaticColorEffectProps StaticColorEffectProps => ((StaticColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.StaticColor)).EffectProperties;

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
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);
            LoadSmoothness();

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
    }
}
