using System;

namespace RestoApp.Presentation.Models;

public class CashTransactionModel
{
    public int Id { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; } = "Efectivo"; // Efectivo, Tarjeta de Débito, Tarjeta de Crédito, Transferencia MP/QR
    public string ProcesadoPor { get; set; } = string.Empty;

    public string HoraFormateada => FechaHora.ToString("HH:mm:ss");
    public string MontoFormateado => $"$ {Monto:N2}";
}
