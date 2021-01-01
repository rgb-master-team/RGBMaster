using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using RGBMasterUWPApp.Pages.EffectsControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace RGBMasterUWPApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EffectsPage : Page
    {
        private EffectMetadata selectedEffectMetadata;

        public readonly Dictionary<EffectType, Type> contentByEffectType = new Dictionary<EffectType, Type>()
        {
            { EffectType.Music, typeof(MusicEffectControl) },
            { EffectType.StaticColor, typeof(StaticColorEffectControl) },
            { EffectType.DominantColor, typeof(DominantDisplayColorEffectControl) },
            { EffectType.CursorColor, typeof(CursorColorEffectControl) },
            { EffectType.Gradient, typeof(GradientEffectControl) }
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

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = (Pivot)sender;

            var selectedPivotItem = (PivotItem)pivot.ItemsPanelRoot.Children.ElementAt(pivot.SelectedIndex);

            selectedEffectMetadata = (EffectMetadata)selectedPivotItem.DataContext;

            if (!contentByEffectType.TryGetValue(selectedEffectMetadata.Type, out var effectType))
            {
                throw new NotImplementedException($"A view for effect {selectedEffectMetadata.EffectName} is not implemented. Implement it and be sure to include it on contentByEffectType.");
            }

            effectControlFrame.Navigate(effectType);
        }

        private void EffectSelectionPivot_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppState.Instance.IsEffectRunning)
            {
                EffectSelectionPivot.SelectedItem = ((IEnumerable<EffectMetadata>)EffectSelectionPivot.ItemsSource).First(item => item.EffectMetadataGuid == AppState.Instance.ActiveEffect?.EffectMetadataGuid);
            }
        }
    }
}
