using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBMasterUWPApp.State
{
    public class RegisteredProvider
    {
        public ProviderMetadata Provider { get; set; }
        public ObservableCollection<DiscoveredDevice> Devices { get; set; }
    }
}
