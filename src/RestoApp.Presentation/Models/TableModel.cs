using CommunityToolkit.Mvvm.ComponentModel;

namespace RestoApp.Presentation.Models;

public partial class TableModel : ObservableObject
{
    public int Id { get; set; }
    public int NroMesa { get; set; }
    public int Capacidad { get; set; }
    public string Ubicacion { get; set; } = "Salón Principal"; // Salón Principal, Terraza, Barra, VIP

    [ObservableProperty]
    private string _estado = "Libre"; // Libre, Reservada, Ocupada, En Limpieza

    [ObservableProperty]
    private string? _clienteAsignado;

    [ObservableProperty]
    private decimal _montoConsumo;

    [ObservableProperty]
    private string? _mozoAsignado;

    public string EstadoBadge => Estado switch
    {
        "Libre" => "🟢 LIBRE",
        "Reservada" => "🔵 RESERVADA",
        "Ocupada" => "🟠 OCUPADA",
        "En Limpieza" => "🔴 EN LIMPIEZA",
        _ => "⚪ UNKNOWN"
    };

    public string DescripcionMesa => $"Mesa #{NroMesa} ({Ubicacion}) - {ClienteAsignado ?? "Cliente Eventual"} - $ {MontoConsumo:N2}";
}
