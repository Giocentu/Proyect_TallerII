using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace RestoApp.Presentation.Converters;

public class BoolToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isTrue && isTrue)
        {
            return new SolidColorBrush(Color.Parse("#10B981")); // Active Green
        }
        return new SolidColorBrush(Color.Parse("#EF4444")); // Inactive Red
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
