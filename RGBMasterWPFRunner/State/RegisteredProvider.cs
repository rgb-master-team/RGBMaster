using Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RGBMasterWPFRunner.State
{
    public class RegisteredProvider
    {
        public Provider Provider { get; set; }
        public ObservableCollection<DiscoveredDevice> Devices { get; set; }
    }
}
