using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RestoApp.Presentation.Models;

public partial class EventModel : ObservableObject
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Descripcion { get; set; } = string.Empty;

    [ObservableProperty]
    private int _reservasVinculadasCount;

    [ObservableProperty]
    private bool _activo = true;

    public string FechaFormateada => Fecha.ToString("dd/MM/yyyy");
}
