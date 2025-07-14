using AppExecutionManager.EventManagement;
using AppExecutionManager.State;
using Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RGBMaster.Pages;

public sealed partial class EffectsPage : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    public ObservableCollection<EffectMetadata> Effects => AppState.Instance.Effects;
    
    private EffectMetadata selectedEffect;

    public EffectsPage()
    {
        this.InitializeComponent();
        AppState.Instance.PropertyChanged += AppState_PropertyChanged;
        UpdateStatus();
    }

    private void AppState_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AppState.ActiveEffect) || e.PropertyName == nameof(AppState.IsEffectRunning))
        {
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        if (AppState.Instance.IsEffectRunning && AppState.Instance.ActiveEffect != null)
        {
            StatusTextBlock.Text = $"Running: {AppState.Instance.ActiveEffect.EffectName}";
        }
        else
        {
            StatusTextBlock.Text = "No effect running";
        }
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedEffect = e.AddedItems[0] as EffectMetadata;
        }
    }

    private void StartEffectButton_Click(object sender, RoutedEventArgs e)
    {
        if (selectedEffect != null)
        {
            EventManager.Instance.RequestEffectActivation(selectedEffect);
        }
    }

    private void StopEffectButton_Click(object sender, RoutedEventArgs e)
    {
        EventManager.Instance.RequestEffectActivation(null);
    }
}