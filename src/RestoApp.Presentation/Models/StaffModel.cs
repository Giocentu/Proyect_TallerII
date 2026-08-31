using CommunityToolkit.Mvvm.ComponentModel;

namespace RestoApp.Presentation.Models;

public partial class StaffModel : ObservableObject
{
    public string Dni { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    
    [ObservableProperty]
    private string _rol = "Mozo"; // Dueño, Gerente, Recepcionista, Cajero, Mozo

    [ObservableProperty]
    private string _turnoInicio = "08:00";

    [ObservableProperty]
    private string _turnoFin = "16:00";

    [ObservableProperty]
    private bool _activoEnRol = true;
}
