using Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RGBMaster.State
{
    public class AppState
    {
        private static readonly AppState instance;

        static AppState()
        {
            instance = new AppState()
            {
                RegisteredProviders = new ObservableCollection<RegisteredProvider>()
            };

        }
        private AppState()
        {
        }
        public static AppState Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<RegisteredProvider> RegisteredProviders { get; set; }
    }
}
