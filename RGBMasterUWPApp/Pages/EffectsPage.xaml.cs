using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace RGBMasterUWPApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EffectsPage : Page
    {
        public EffectsPage()
        {
            this.InitializeComponent();
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            var color = System.Drawing.Color.FromArgb(sender.Color.R, sender.Color.G, sender.Color.B);
            EventManager.Instance.ChangeStaticColor(new StaticColorEffectProps() { SelectedColor = color, SelectedBrightness = AppState.Instance.StaticColorEffectProperties.SelectedBrightness });
          
            /*AppState.Instance.StaticColor = color;

            if (AppState.Instance.IsEffectRunning)
            {
                var staticColorEffect = AppState.Instance.SelectedEffect as StaticColorEffectMetadata;
                staticColorEffect.UpdateProps(new StaticColorEffectProps() { SelectedColor = color });

                EventManager.Instance.UpdateEffect(staticColorEffect);
            }*/
        }

        private void ChangeCurrentRunningEffect(EffectMetadata desiredEffect)
        {
            EventManager.Instance.UpdateEffect(desiredEffect);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = (Pivot)sender;
            EffectMetadata effectMd;

            // TODO - REFACTOR THE WHOLE UI TO TAKE ITS SHIT FROM THE FUCKING STATE AND NOT HARD CODED!#@#@!!@#!@#
            switch (pivot.SelectedIndex)
            {
                case 0:
                    effectMd = AppState.Instance.Effects.First(effect => effect.GetType() == typeof(StaticColorEffectMetadata));
                    break;
                case 2:
                    effectMd = AppState.Instance.Effects.First(effect => effect.GetType() == typeof(MusicEffectMetadata));
                    break;
                case 3:
                    effectMd = AppState.Instance.Effects.First(effect => effect.GetType() == typeof(DominantDisplayColorEffectMetadata));
                    break;
                default:
                    throw new NotImplementedException("This effect is not implemented!");
            }

            // This is obviously a lazy design. TODO - add types for all effects and take the time to
            // reimplement the way we apply effects, instead of reinstantiating them all the time.
            // perhaps a factory.
            if (effectMd.GetType() != AppState.Instance.SelectedEffect?.GetType())
            {
                ChangeCurrentRunningEffect(effectMd);
            }
        }

        private void Brightness_Value_Changed(object sender, RangeBaseValueChangedEventArgs e)
        {
            EventManager.Instance.ChangeStaticColor(new StaticColorEffectProps() { SelectedColor = AppState.Instance.StaticColorEffectProperties.SelectedColor, SelectedBrightness = (byte)Brighness_Slider.Value });

        }

    }
}
