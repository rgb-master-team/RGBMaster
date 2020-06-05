using Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBMaster.State
{
    public class AppState
    {
        private static readonly AppState instance = new AppState()
        {
            RegisteredProviders = new ObservableCollection<RegisteredProvider>()
        };

        static AppState()
        {
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
