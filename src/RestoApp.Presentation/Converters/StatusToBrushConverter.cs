using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace RestoApp.Presentation.Converters;

public class StatusToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "Libre" => new SolidColorBrush(Color.Parse("#10B981")),      // Emerald Green
                "Reservada" => new SolidColorBrush(Color.Parse("#3B82F6")),  // Royal Blue
                "Ocupada" => new SolidColorBrush(Color.Parse("#F59E0B")),    // Amber Orange
                "En Limpieza" => new SolidColorBrush(Color.Parse("#EF4444")),// Rose Red
                
                "Confirmada" => new SolidColorBrush(Color.Parse("#10B981")),
                "En Espera" => new SolidColorBrush(Color.Parse("#8B5CF6")),
                "Cancelada" => new SolidColorBrush(Color.Parse("#64748B")),
                "Completada" => new SolidColorBrush(Color.Parse("#06B6D4")),
                
                _ => new SolidColorBrush(Color.Parse("#94A3B8"))
            };
        }
        return new SolidColorBrush(Color.Parse("#94A3B8"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
