using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class CashierViewModel : ViewModelBase
{
    [ObservableProperty]
    private ShiftBalanceModel _shiftBalance = new();

    [ObservableProperty]
    private ObservableCollection<CashTransactionModel> _transactions = new();

    [ObservableProperty]
    private ObservableCollection<TableModel> _unpaidTables = new();

    [ObservableProperty]
    private TableModel? _selectedTableToPay;

    [ObservableProperty]
    private string _selectedPaymentMethod = "Efectivo";

    [ObservableProperty]
    private decimal _paymentAmount = 0.00m;

    [ObservableProperty]
    private string _countedCashInput = "15000";

    [ObservableProperty]
    private string _feedbackMessage = string.Empty;

    [ObservableProperty]
    private bool _hasFeedback = false;

    public string[] PaymentMethods => new[] { "Efectivo", "Tarjeta de Débito", "Tarjeta de Crédito", "Transferencia MP/QR" };

    public CashierViewModel()
    {
        LoadMockData();
    }

    private void LoadMockData()
    {
        UnpaidTables = new ObservableCollection<TableModel>
        {
            new TableModel { Id = 2, NroMesa = 2, Ubicacion = "Salón Principal", ClienteAsignado = "Juan Pérez", MontoConsumo = 24500.00m, Estado = "Ocupada" },
            new TableModel { Id = 6, NroMesa = 6, Ubicacion = "Terraza", ClienteAsignado = "Carlos V.", MontoConsumo = 38900.00m, Estado = "Ocupada" },
            new TableModel { Id = 9, NroMesa = 9, Ubicacion = "Barra", ClienteAsignado = "Gonzalo T.", MontoConsumo = 12000.00m, Estado = "Ocupada" }
        };

        Transactions = new ObservableCollection<CashTransactionModel>
        {
            new CashTransactionModel { Id = 1, FechaHora = DateTime.Now.AddHours(-2), Descripcion = "Cobro Mesa #1 - Reserva #98", Monto = 18500.00m, MetodoPago = "Efectivo", ProcesadoPor = "Martín (Cajero)" },
            new CashTransactionModel { Id = 2, FechaHora = DateTime.Now.AddHours(-1), Descripcion = "Cobro Mesa #5 - Cliente Gómez", Monto = 31200.00m, MetodoPago = "Tarjeta de Crédito", ProcesadoPor = "Martín (Cajero)" },
            new CashTransactionModel { Id = 3, FechaHora = DateTime.Now.AddMinutes(-30), Descripcion = "Cobro Mesa #10 - Evento Jazz", Monto = 45000.00m, MetodoPago = "Transferencia MP/QR", ProcesadoPor = "Martín (Cajero)" }
        };

        RecalculateTotals();
        SelectedTableToPay = UnpaidTables.FirstOrDefault();
    }

    partial void OnSelectedTableToPayChanged(TableModel? value)
    {
        if (value != null)
        {
            PaymentAmount = value.MontoConsumo;
        }
    }

    private void RecalculateTotals()
    {
        ShiftBalance.TotalEfectivo = Transactions.Where(t => t.MetodoPago == "Efectivo").Sum(t => t.Monto);
        ShiftBalance.TotalTarjeta = Transactions.Where(t => t.MetodoPago.StartsWith("Tarjeta")).Sum(t => t.Monto);
        ShiftBalance.TotalTransferencia = Transactions.Where(t => t.MetodoPago.StartsWith("Transferencia")).Sum(t => t.Monto);
        
        OnPropertyChanged(nameof(ShiftBalance));
    }

    [RelayCommand]
    private void ProcessPayment()
    {
        if (SelectedTableToPay == null)
        {
            FeedbackMessage = "❌ Por favor, seleccione una mesa o reserva para cobrar.";
            HasFeedback = true;
            return;
        }

        if (PaymentAmount <= 0)
        {
            FeedbackMessage = "❌ El monto a cobrar debe ser mayor a 0.";
            HasFeedback = true;
            return;
        }

        var newTx = new CashTransactionModel
        {
            Id = Transactions.Count + 1,
            FechaHora = DateTime.Now,
            Descripcion = $"Cobro Mesa #{SelectedTableToPay.NroMesa} - {SelectedTableToPay.ClienteAsignado ?? "Cliente Eventual"}",
            Monto = PaymentAmount,
            MetodoPago = SelectedPaymentMethod,
            ProcesadoPor = "Martín (Cajero)"
        };

        Transactions.Insert(0, newTx);
        UnpaidTables.Remove(SelectedTableToPay);
        RecalculateTotals();

        FeedbackMessage = $"💳 ¡Pago de $ {PaymentAmount:N2} ({SelectedPaymentMethod}) procesado exitosamente! Comprobante #{newTx.Id} generado.";
        HasFeedback = true;

        SelectedTableToPay = UnpaidTables.FirstOrDefault();
    }

    [RelayCommand]
    private void CalculateClosure()
    {
        if (decimal.TryParse(CountedCashInput, out decimal counted))
        {
            ShiftBalance.EfectivoContado = counted;
            decimal diff = ShiftBalance.Diferencia;
            if (diff == 0)
            {
                FeedbackMessage = "✅ Arqueo de caja perfecto. El efectivo contado coincide con el esperado.";
            }
            else if (diff > 0)
            {
                FeedbackMessage = $"ℹ️ Arqueo finalizado con sobrante de $ {diff:N2}.";
            }
            else
            {
                FeedbackMessage = $"⚠️ ALERTA DE CAJA: Discrepancia detectada. Faltante de $ {Math.Abs(diff):N2}.";
            }
            HasFeedback = true;
        }
        else
        {
            FeedbackMessage = "❌ Por favor ingrese un monto de efectivo contado válido.";
            HasFeedback = true;
        }
    }

    [RelayCommand]
    private void CloseShift()
    {
        ShiftBalance.CajaAbierta = false;
        FeedbackMessage = "🔒 Turno de caja CERRADO. Se ha registrado la liquidación del turno.";
        HasFeedback = true;
    }
}
