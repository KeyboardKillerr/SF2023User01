using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SF2023User01.WpfApp.Converters;

internal class QuantityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int quantity)
        {
            SolidColorBrush color = quantity < 1 ? new(Colors.Gray) : new(Colors.White);
            return color;
        }
        return new SolidColorBrush(Colors.Green);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
