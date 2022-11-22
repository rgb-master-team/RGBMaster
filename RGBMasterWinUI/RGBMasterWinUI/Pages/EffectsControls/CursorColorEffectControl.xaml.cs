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
    public sealed partial class CursorColorEffectControl : Page
    {
        private const string RelativeSmoothnessUserSettingKey = "CursorColorEffectRelativeSmoothness";
        private const string SyncBrightnessByHSLUserSettingKey = "CursorColorEffectSyncBrightnessByHSL";

        public event PropertyChangedEventHandler PropertyChanged;

        public CursorColorEffectMetadata CursorColorEffectMd => (CursorColorEffectMetadata)AppState.Instance.Effects.First(effect => effect.Type == EffectType.CursorColor);
        public CursorColorEffectMetadataProperties CursorColorEffectProps => CursorColorEffectMd.EffectProperties;

        public int RelativeSmoothness
        {
            get
            {
                return CursorColorEffectProps.RelativeSmoothness;
            }
            set
            {
                CursorColorEffectProps.RelativeSmoothness = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public bool SyncBrightnessByHSL
        {
            get
            {
                return CursorColorEffectProps.SyncBrightnessByHSL;
            }
            set
            {
                CursorColorEffectProps.SyncBrightnessByHSL = value;
                NotifyPropertyChangedUtils.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public CursorColorEffectControl()
        {
            EventManager.Instance.SubscribeToAppClosingTriggers(AppClosingTriggered);

            LoadSettings();

            this.InitializeComponent();
        }

        private void LoadSettings()
        {
            LoadRelativeSmoothness();
            LoadSyncBrightnessByHSL();
        }

        private void LoadSyncBrightnessByHSL()
        {
            EventManager.Instance.LoadUserSetting(SyncBrightnessByHSLUserSettingKey);

            if (AppState.Instance.UserSettingsCache.TryGetValue(SyncBrightnessByHSLUserSettingKey, out var syncBrightnessByHsl))
            {
                SyncBrightnessByHSL = (bool)syncBrightnessByHsl;
            }
            else
            {
                SyncBrightnessByHSL = true;
            }
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

        private void AppClosingTriggered(object sender, EventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void SaveUserSettingsForPage()
        {
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(RelativeSmoothnessUserSettingKey, RelativeSmoothness));
            EventManager.Instance.StoreUserSetting(new Tuple<string, object>(SyncBrightnessByHSLUserSettingKey, SyncBrightnessByHSL));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveUserSettingsForPage();
        }

        private void EffectControl_ResetButton_Clicked(object sender, EventArgs e)
        {
            EventManager.Instance.ResetUserSettingsToDefault(new List<string>() { RelativeSmoothnessUserSettingKey, SyncBrightnessByHSLUserSettingKey });
            LoadSettings();
        }
    }
}
