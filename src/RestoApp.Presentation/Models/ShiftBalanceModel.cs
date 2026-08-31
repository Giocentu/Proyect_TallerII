using CommunityToolkit.Mvvm.ComponentModel;

namespace RestoApp.Presentation.Models;

public partial class ShiftBalanceModel : ObservableObject
{
    [ObservableProperty]
    private decimal _montoInicial = 15000.00m;

    [ObservableProperty]
    private decimal _totalEfectivo = 0.00m;

    [ObservableProperty]
    private decimal _totalTarjeta = 0.00m;

    [ObservableProperty]
    private decimal _totalTransferencia = 0.00m;

    [ObservableProperty]
    private decimal _efectivoContado = 0.00m;

    [ObservableProperty]
    private bool _cajaAbierta = true;

    public decimal TotalCobrado => TotalEfectivo + TotalTarjeta + TotalTransferencia;
    public decimal EfectivoEsperado => MontoInicial + TotalEfectivo;
    public decimal Diferencia => EfectivoContado - EfectivoEsperado;
}
