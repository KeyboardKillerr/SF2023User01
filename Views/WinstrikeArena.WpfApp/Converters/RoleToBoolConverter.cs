using DataModel.Entities;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SF2023User01.WpfApp.Converters;

internal class RoleToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is User user)
        {
            if (user.Role == "Admin") return Visibility.Visible;
            else return Visibility.Collapsed;
        }
        else if (value is null) return Visibility.Collapsed;
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
