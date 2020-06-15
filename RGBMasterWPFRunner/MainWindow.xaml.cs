using AppExecutionManager.EventManagement;
using Infrastructure;
using NAudio.Wave;
using RGBMasterUWPApp.State;
using System.Collections.Generic;
using Windows.ApplicationModel;

namespace RGBMasterWPFRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;


            AppState.Instance.AppVersion = string.Format($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");

            EventManager.Instance.SubscribeToEffectChanged(Instance_EffectChanged);
            EventManager.Instance.SubscribeToSelectedDevicesChanged(Instance_SelectedDevicesChanged);
        }

        private void Instance_EffectChanged(object sender, Infrastructure.EffectMetadata e)
        {
            throw new System.NotImplementedException();
        }

        private void Instance_SelectedDevicesChanged(object sender, List<Device> e)
        {
            throw new System.NotImplementedException();
        }
    }
}
