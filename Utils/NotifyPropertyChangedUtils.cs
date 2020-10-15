using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Utils
{
    public static class NotifyPropertyChangedUtils
    {
        public static void OnPropertyChanged(PropertyChangedEventHandler propertyChanged, object sender, [CallerMemberName] string name = null)
        {
            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }
    }
}
