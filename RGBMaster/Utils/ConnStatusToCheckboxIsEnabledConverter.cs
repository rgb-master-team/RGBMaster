using Common;
using Microsoft.UI.Xaml.Data;
using System;

namespace RGBMaster.Utils;

public class ConnStatusToCheckboxIsEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var connStatus = (ConnectionStatus)value;
        return connStatus == ConnectionStatus.Connected || connStatus == ConnectionStatus.Disconnected;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}