using AppExecutionManager.EventManagement;
using Infrastructure;
using RGBMasterUWPApp.State;
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
            AppState.Instance.StaticColor = color;

            if (AppState.Instance.IsEffectRunning)
            {
                var staticColorEffect = AppState.Instance.SelectedEffect as StaticColorEffectMetadata;
                staticColorEffect.UpdateProps(new StaticColorEffectProps() { SelectedColor = color });

                EventManager.Instance.UpdateEffect(staticColorEffect);
            }
        }

        private void ChangeCurrentRunningEffect(EffectMetadata desiredEffect)
        {
            AppState.Instance.SelectedEffect = desiredEffect;

            EventManager.Instance.UpdateEffect(desiredEffect);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = (Pivot)sender;
            EffectMetadata effect;

            switch (pivot.SelectedIndex)
            {
                case 0:
                    // Color picker Logic here
                    effect = new StaticColorEffectMetadata();
                    break;
                case 1:
                    // Music Logic Here
                    effect = new MusicEffectMetadata();
                    break;
                case 2:
                    // Pointer Logic here
                    effect = new DominantDisplayColorEffect();
                    break;
                default:
                    effect = new StaticColorEffectMetadata();
                    break;
            }

            // This is obviously a lazy design. TODO - add types for all effects and take the time to
            // reimplement the way we apply effects, instead of reinstantiating them all the time.
            // perhaps a factory.
            if (effect.GetType() != AppState.Instance.SelectedEffect?.GetType())
            {
                ChangeCurrentRunningEffect(effect);
            }
        }
    }
}
