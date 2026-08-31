using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace RestoApp.Presentation.Converters;

public class BoolToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isTrue)
        {
            return isTrue ? "Activo" : "Inactivo";
        }
        return "Inactivo";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
