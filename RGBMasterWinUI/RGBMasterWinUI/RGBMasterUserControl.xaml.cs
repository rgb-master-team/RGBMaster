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
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI
{
    public sealed partial class RGBMasterUserControl : UserControl
    {
        public RGBMasterUserControl()
        {
            this.InitializeComponent();
        }

        public string CurrentEffectName
        {
            get
            {
                return AppState.Instance.ActiveEffect?.EffectName;
            }
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly Dictionary<string, Type> pageToType = new Dictionary<string, Type>()
                {
                    { nameof(DevicesPage), typeof(DevicesPage) },
                    { nameof(EffectsPage), typeof(EffectsPage) },
                    { nameof(SettingsPage), typeof(SettingsPage) }
                };

        private SemaphoreSlim startAndStopSemaphore = new SemaphoreSlim(1, 1);

        private void MainAppContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            MainNavigationView.SelectedItem = MainNavigationView.MenuItems[0];
        }

        private void MainNavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            string selectionTag;

            if (args.IsSettingsSelected == true)
            {
                selectionTag = "SettingsPage";
            }
            else
            {
                selectionTag = (string)args.SelectedItemContainer.Tag;
            }

            var navigationResult = MainAppContentFrame.Navigate(pageToType[selectionTag], null, args.RecommendedNavigationTransitionInfo);
        }


        private void RGBMasterUserControl_Loaded(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            EventManager.Instance.InitializeProviders();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
