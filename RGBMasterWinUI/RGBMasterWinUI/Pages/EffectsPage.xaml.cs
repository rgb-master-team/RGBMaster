// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RGBMasterWinUI.Pages.EffectsControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI.Pages
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
