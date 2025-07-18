using Common;
using Microsoft.UI.Xaml.Data;
using System;

namespace RGBMaster.Utils;

public class ConnStatusToProgBarIsActiveConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var connStatus = (ConnectionStatus)value;
        return connStatus == ConnectionStatus.Connecting || connStatus == ConnectionStatus.Disconnecting;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}