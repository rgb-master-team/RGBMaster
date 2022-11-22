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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RGBMasterWinUI.Pages.EffectsControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DominantDisplayColorEffectControl : Page
    {
        private const string RelativeSmoothnessUserSettingKey = "DominantDisplayColorEffectRelativeSmoothness";
        private const string SyncBrightnessByHSLUserSettingKey = "DominantDisplayColorEffectSyncBrightnessByHSL";

        public event PropertyChangedEventHandler PropertyChanged;

        public DominantDisplayColorEffectMetadata DominantDisplayColorEffectMd => (DominantDisplayColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.DominantColor);
        public DominantDisplayColorEffectMetadataProperties DominantDisplayColorEffectProps => DominantDisplayColorEffectMd.EffectProperties;

        public int RelativeSmoothness
        {
            get
            {
                return DominantDisplayColorEffectProps.RelativeSmoothness;
            }
            set
            {
                DominantDisplayColorEffectProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public bool SyncBrightnessByHSL
        {
            get
            {
                return DominantDisplayColorEffectProps.SyncBrightnessByHSL;
            }
            set
            {
                DominantDisplayColorEffectProps.SyncBrightnessByHSL = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public DominantDisplayColorEffectControl()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);

            LoadSettings();

            this.InitializeComponent();
        }

        private void LoadSettings()
        {
            LoadSyncBrightnessByHSL();
            LoadRelativeSmoothness();
        }

        private void LoadRelativeSmoothness()
        {
            EventManager.Instance.LoadUserSetting(RelativeSmoothnessUserSettingKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(RelativeSmoothnessUserSettingKey, out var relativeSmoothness))
            {
                RelativeSmoothness = (int)relativeSmoothness;
            }
            else
            {
                RelativeSmoothness = 0;
            }
        }

        private void LoadSyncBrightnessByHSL()
        {
            EventManager.Instance.LoadUserSetting(SyncBrightnessByHSLUserSettingKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(SyncBrightnessByHSLUserSettingKey, out var syncBrightnessByHSL))
            {
                SyncBrightnessByHSL = (bool)syncBrightnessByHSL;
            }
            else
            {
                SyncBrightnessByHSL = true;
            }
        }

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(SyncBrightnessByHSLUserSettingKey, SyncBrightnessByHSL));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(RelativeSmoothnessUserSettingKey, RelativeSmoothness));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void EffectControl_ResetButton_Clicked(object sender, EventArgs e)
        {
            EventManager.Instance.ResetUserSettingsToDefault(new List<string>() { SyncBrightnessByHSLUserSettingKey, RelativeSmoothnessUserSettingKey });
            LoadSettings();
        }
    }
}
