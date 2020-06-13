using Infrastructure;
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
        public Provider Provider { get; set; }
        public ObservableCollection<DiscoveredDevice> Devices { get; set; }
    }
}
