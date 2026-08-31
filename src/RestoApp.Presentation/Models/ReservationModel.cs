using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RestoApp.Presentation.Models;

public partial class ReservationModel : ObservableObject
{
    public int Id { get; set; }
    public string ClienteDni { get; set; } = string.Empty;
    public string ClienteNombre { get; set; } = string.Empty;
    public string ClienteTelefono { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public int CantidadPersonas { get; set; } = 2;
    public string UbicacionSector { get; set; } = "Salón Principal";
    public string MesasAsignadas { get; set; } = "Mesa 1";
    public DateTime FechaMaxCancelacion { get; set; } = DateTime.Now.AddHours(-48);
    public string? EventoVinculado { get; set; }
    public string EmpleadoCarga { get; set; } = "Sistema";

    [ObservableProperty]
    private string _estado = "Confirmada"; // Confirmada, En Espera, Cancelada, Completada

    public string FechaFormateada => FechaHora.ToString("dd/MM/yyyy HH:mm");
    public string CancelacionFormateada => FechaMaxCancelacion.ToString("dd/MM/yyyy HH:mm");
}
