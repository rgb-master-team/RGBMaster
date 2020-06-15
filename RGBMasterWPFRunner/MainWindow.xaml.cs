using NAudio.Wave;
using RGBMasterUWPApp.State;
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


            AppState.Instance.AppVersion = version;
        }
    }
}
