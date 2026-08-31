using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace RestoApp.Presentation.Converters;

public class RoleToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? currentRole = value as string;
        string? allowedRolesStr = parameter as string;

        if (string.IsNullOrEmpty(currentRole) || string.IsNullOrEmpty(allowedRolesStr))
            return true;

        // E.g. parameter = "Dueño,Gerente"
        var allowedRoles = allowedRolesStr.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        foreach (var role in allowedRoles)
        {
            if (string.Equals(role, currentRole, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
