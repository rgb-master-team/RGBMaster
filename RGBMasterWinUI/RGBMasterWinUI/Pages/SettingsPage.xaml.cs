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
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public sealed partial class SettingsPage : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string ToggleDeviceOnCheckUserConfigKey = "ToggleDeviceOnCheck";
        private const string LogPathKey = "LogPath";
        private const string IsDarkModeKey = "IsDarkMode";


        private string logPath;
        private bool toggleDeviceOnCheck;
        private bool isDarkMode;

        public bool IsDarkMode
        {
            get
            {
                return isDarkMode;
            }
            set
            {
                isDarkMode = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public string LogPath
        {
            get
            {
                return logPath;
            }
            set
            {
                logPath = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public ObservableCollection<ProviderMetadata> SupportedProviders
        {
            get
            {
                return AppState.Instance.SupportedProviders;
            }
        }

        public string AppVersion
        {
            get
            {
                return AppState.Instance.AppVersion;
            }
        }

        public bool ToggleDeviceOnCheckUser
        {
            get
            {
                return toggleDeviceOnCheck;
            }
            set
            {
                toggleDeviceOnCheck = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public SettingsPage()
        {
            this.InitializeComponent();

            LoadSettings();
        }

        private void LoadSettings()
        {
            EventManager.Instance.LoadUserSetting(ToggleDeviceOnCheckUserConfigKey);
            ToggleDeviceOnCheckUser = AppState.Instance.UserSettingsCache.TryGetValue(ToggleDeviceOnCheckUserConfigKey, out var shouldToggleDeviceOnCheck) ? (bool)shouldToggleDeviceOnCheck : true;

            EventManager.Instance.LoadUserSetting(LogPathKey);
            LogPath = AppState.Instance.UserSettingsCache.TryGetValue(LogPathKey, out var logPathKeyObj) && !string.IsNullOrWhiteSpace(logPathKeyObj as string) ? (string)logPathKeyObj : null;

            EventManager.Instance.LoadUserSetting(IsDarkModeKey);
            IsDarkMode = AppState.Instance.UserSettingsCache.TryGetValue(IsDarkModeKey, out var isDarkModeObj) ? (bool)isDarkModeObj : true;
        }

        private async void GitHub_Button_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/rgb-master-team/RGBMaster"));
        }

        private async void Discord_Button_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://discord.gg/zWbe3UV"));
        }

        private void Check_Turn_On_Device_When_Checked()
        {
            if (TurnOnDeviceEnabler.IsOn)
            { }
        }

        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedProvider = (ProviderMetadata)e.ClickedItem;

            if (clickedProvider.ProviderUrl == null)
            {
                return;
            }

            await Windows.System.Launcher.LaunchUriAsync(new Uri(clickedProvider.ProviderUrl));
        }

        private void TurnOnDeviceEnabler_Toggled(object sender, RoutedEventArgs e)
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(ToggleDeviceOnCheckUserConfigKey, TurnOnDeviceEnabler.IsOn));
        }

        private void BrowseLogPath_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LightOrDarkToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(IsDarkModeKey, LightOrDarkToggleSwitch.IsOn));
        }

        private void ResetButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            EventManager.Instance.ResetUserSettingsToDefault(new List<string> { ToggleDeviceOnCheckUserConfigKey, IsDarkModeKey, LogPathKey });
            LoadSettings();
        }
    }
}
