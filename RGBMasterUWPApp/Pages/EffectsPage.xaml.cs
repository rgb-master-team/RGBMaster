using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using RGBMasterUWPApp.Pages.EffectsControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public readonly Dictionary<EffectType, Type> contentByEffectType = new Dictionary<EffectType, Type>()
        {
            { EffectType.Music, typeof(MusicEffectControl) },
            { EffectType.StaticColor, typeof(StaticColorEffectControl) },
            { EffectType.DominantColor, typeof(DominantDisplayColorEffectControl) },
            { EffectType.CursorColor, typeof(CursorColorEffectControl) }
        };

        public ObservableCollection<EffectMetadata> SupportedEffects
        {
            get
            {
                return AppState.Instance.Effects;
            }
        }

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

            var selectedPivotItem = (PivotItem)pivot.ItemsPanelRoot.Children.ElementAt(pivot.SelectedIndex);

            var newEffectMetadata = (EffectMetadata)selectedPivotItem.DataContext;

            // This is obviously a lazy design. TODO - add types for all effects and take the time to
            // reimplement the way we apply effects, instead of reinstantiating them all the time.
            // perhaps a factory.
            if (newEffectMetadata.EffectMetadataGuid != AppState.Instance.SelectedEffect?.EffectMetadataGuid)
            {
                ChangeCurrentRunningEffect(newEffectMetadata);
            }

            if (effectControlFrame == null)
            {
                return;
            }

            if (!contentByEffectType.TryGetValue(newEffectMetadata.Type, out var effectType))
            {
                throw new NotImplementedException($"A view for effect {newEffectMetadata.EffectName} is not implemented. Implement it and be sure to include it on contentByEffectType.");
            }

            effectControlFrame.Navigate(effectType);
        }
    }
}
