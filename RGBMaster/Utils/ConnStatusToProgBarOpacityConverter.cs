using Common;
using Microsoft.UI.Xaml.Data;
using System;

namespace RGBMaster.Utils;

public class ConnStatusToProgBarOpacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var connStatus = (ConnectionStatus)value;

        if (connStatus == ConnectionStatus.Connecting || connStatus == ConnectionStatus.Disconnecting)
        {
            return 1.0;
        }

        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}