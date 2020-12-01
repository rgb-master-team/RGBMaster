using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Utils;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RGBMasterUWPApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page, INotifyPropertyChanged
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
            EventManager.Instance.LoadUserSetting(ToggleDeviceOnCheckUserConfigKey);
            ToggleDeviceOnCheckUser = AppState.Instance.UserSettingsCache.TryGetValue(ToggleDeviceOnCheckUserConfigKey, out var shouldToggleDeviceOnCheck) ? (bool)shouldToggleDeviceOnCheck : true;

            EventManager.Instance.LoadUserSetting(LogPathKey);
            LogPath = AppState.Instance.UserSettingsCache.TryGetValue(LogPathKey, out var logPathKeyObj) && !string.IsNullOrWhiteSpace(logPathKeyObj as string) ? (string)logPathKeyObj : null;

            EventManager.Instance.LoadUserSetting(IsDarkModeKey);
            IsDarkMode = AppState.Instance.UserSettingsCache.TryGetValue(IsDarkModeKey, out var isDarkModeObj) ? (bool)isDarkModeObj : true;

            this.InitializeComponent();
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

        private async void BrowseLogPath_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                
                // TODO.
            }
            else
            {
                // TODO.
            }
        }

        private void LightOrDarkToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(IsDarkModeKey, LightOrDarkToggleSwitch.IsOn));
        }
    }
}
