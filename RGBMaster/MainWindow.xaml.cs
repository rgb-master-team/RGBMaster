using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using AppExecutionManager.State;
using System;
using System.ComponentModel;

namespace RGBMaster;

public sealed partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public string CurrentEffectName => AppState.Instance.ActiveEffect?.EffectName ?? "";

    public MainWindow()
    {
        this.InitializeComponent();
        this.Title = "RGB Master";
        
        AppState.Instance.PropertyChanged += (s, e) => 
        {
            if (e.PropertyName == nameof(AppState.ActiveEffect))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentEffectName)));
            }
        };

        MainNavigationView.SelectedItem = DevicesNavigationItem;
        NavigateToPage(typeof(DevicesPage));
    }

    private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item)
        {
            var tag = item.Tag?.ToString();
            switch (tag)
            {
                case "DevicesPage":
                    NavigateToPage(typeof(DevicesPage));
                    break;
                case "EffectsPage":
                    NavigateToPage(typeof(EffectsPage));
                    break;
            }
        }
    }

    private void NavigateToPage(Type pageType)
    {
        MainAppContentFrame.Navigate(pageType);
    }

    private void MainAppContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception($"Failed to load Page {e.SourcePageType.Name}");
    }
}