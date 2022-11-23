// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI.Pages.EffectsControls
{
    [ContentProperty(Name = "MainContent")]
    public sealed partial class EffectControl : UserControl
    {
        public event EventHandler ResetButton_Clicked;

        public static DependencyProperty MainContentProperty =
                DependencyProperty.Register("MainContent", typeof(object), typeof(EffectControl), null);

        public object MainContent
        {
            get => GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        public static DependencyProperty ShouldShowResetButtonProperty =
                DependencyProperty.Register("ShouldShowResetButton", typeof(bool), typeof(EffectControl), null);

        public bool ShouldShowResetButton
        {
            get => (bool)GetValue(ShouldShowResetButtonProperty);
            set => SetValue(ShouldShowResetButtonProperty, value);
        }

        public static DependencyProperty IsEffectActivationEnabledProperty =
                DependencyProperty.Register("IsEffectActivationEnabled", typeof(bool), typeof(EffectControl), null);

        public bool IsEffectActivationEnabled
        {
            get => (bool)GetValue(IsEffectActivationEnabledProperty);
            set => SetValue(IsEffectActivationEnabledProperty, value);
        }

        public static DependencyProperty EffectMetadataProperty =
                DependencyProperty.Register("EffectMetadata", typeof(EffectMetadata), typeof(EffectControl), null);

        public EffectMetadata EffectMetadata
        {
            get => (EffectMetadata)GetValue(EffectMetadataProperty);
            set => SetValue(EffectMetadataProperty, value);
        }

        public EffectControl()
        {
            this.InitializeComponent();
            IsEffectActivationEnabled = true;
            ShouldShowResetButton = false;
            Loaded += EffectControl_Loaded;
        }

        private void SetEffectActivationPropsByState(bool isEffectRunning)
        {
            if (isEffectRunning)
            {
                EffectActivationButton.IsChecked = true;
            }
            else
            {
                EffectActivationButton.IsChecked = false;
            }
        }

        private void EffectControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetEffectActivationPropsByState(AppState.Instance.ActiveEffect?.EffectMetadataGuid == EffectMetadata.EffectMetadataGuid);
        }

        private void EffectActivationButton_Click(object sender, RoutedEventArgs e)
        {
            var toggle = sender as AppBarToggleButton;
            if (!toggle.IsLoaded)
            {
                return;
            }

            var newEffectMetadata = EffectMetadata;

            var selectedEffectMetadata = AppState.Instance.ActiveEffect;

            // If the active effect is **THIS** effect
            if (selectedEffectMetadata?.EffectMetadataGuid == newEffectMetadata.EffectMetadataGuid)
            {
                // and the toggle has been turned off - deactivate it.
                if (toggle.IsChecked == false)
                {
                    EventManager.Instance.RequestEffectActivation(null);
                }
            }
            // If the active effect is NOT **THIS** effect
            else if (selectedEffectMetadata?.EffectMetadataGuid != newEffectMetadata.EffectMetadataGuid)
            {
                // ... and the toggle has been turned on - activate it.
                if (toggle.IsChecked == true)
                {
                    EventManager.Instance.RequestEffectActivation(newEffectMetadata);
                }
            }
        }

        private void ResetButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            ResetButton_Clicked?.Invoke(sender, null);
        }

        private void ControlsCommandBar_Closed(object sender, object e)
        {
            ControlsCommandBar.IsOpen = true;
        }
    }
}
