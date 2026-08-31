using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class ReservationsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ReservationModel> _allReservations = new();

    [ObservableProperty]
    private ObservableCollection<ReservationModel> _filteredReservations = new();

    [ObservableProperty]
    private ReservationModel? _selectedReservation;

    // Form Fields
    [ObservableProperty]
    private string _newDni = string.Empty;

    [ObservableProperty]
    private string _newNombre = string.Empty;

    [ObservableProperty]
    private string _newTelefono = string.Empty;

    [ObservableProperty]
    private DateTimeOffset _newFecha = DateTimeOffset.Now.AddDays(1);

    [ObservableProperty]
    private TimeSpan _newHora = new TimeSpan(21, 0, 0);

    [ObservableProperty]
    private int _newComensales = 4;

    [ObservableProperty]
    private string _newSector = "Salón Principal";

    [ObservableProperty]
    private string _newMesas = "Mesa 3, Mesa 4";

    [ObservableProperty]
    private string? _selectedEvent = "Ninguno";

    [ObservableProperty]
    private DateTime _computedMaxCancellation = DateTime.Now;

    [ObservableProperty]
    private string _searchFilter = string.Empty;

    [ObservableProperty]
    private string _statusFilter = "Todos";

    [ObservableProperty]
    private string _feedbackMessage = string.Empty;

    [ObservableProperty]
    private bool _hasFeedback = false;

    public string[] Sectors => new[] { "Salón Principal", "Terraza", "Barra", "VIP" };
    public string[] Events => new[] { "Ninguno", "Día del Amigo", "Año Nuevo", "Cena Show Jazz", "San Valentín" };
    public string[] Statuses => new[] { "Todos", "Confirmada", "En Espera", "Cancelada", "Completada" };

    public ReservationsViewModel()
    {
        LoadMockReservations();
        UpdateMaxCancellation();
        ApplyFilter();
    }

    private void LoadMockReservations()
    {
        AllReservations = new ObservableCollection<ReservationModel>
        {
            new ReservationModel
            {
                Id = 101,
                ClienteDni = "32145678",
                ClienteNombre = "Ana Rossi",
                ClienteTelefono = "11-4567-8901",
                FechaHora = DateTime.Now.AddDays(1).Date.AddHours(21),
                CantidadPersonas = 4,
                UbicacionSector = "Salón Principal",
                MesasAsignadas = "Mesa 3",
                FechaMaxCancelacion = DateTime.Now.AddDays(1).Date.AddHours(21).AddHours(-48),
                Estado = "Confirmada",
                EventoVinculado = "Cena Show Jazz",
                EmpleadoCarga = "Sofía (Recepcionista)"
            },
            new ReservationModel
            {
                Id = 102,
                ClienteDni = "28999888",
                ClienteNombre = "Roberto Martínez",
                ClienteTelefono = "11-9876-5432",
                FechaHora = DateTime.Now.AddDays(2).Date.AddHours(22),
                CantidadPersonas = 6,
                UbicacionSector = "Terraza",
                MesasAsignadas = "Mesa 7",
                FechaMaxCancelacion = DateTime.Now.AddDays(2).Date.AddHours(22).AddHours(-48),
                Estado = "En Espera",
                EventoVinculado = null,
                EmpleadoCarga = "Laura (Gerente)"
            },
            new ReservationModel
            {
                Id = 103,
                ClienteDni = "40111222",
                ClienteNombre = "Empresa ACME S.A.",
                ClienteTelefono = "11-3333-2222",
                FechaHora = DateTime.Now.AddDays(3).Date.AddHours(20),
                CantidadPersonas = 10,
                UbicacionSector = "VIP",
                MesasAsignadas = "Mesa 10, Mesa 11",
                FechaMaxCancelacion = DateTime.Now.AddDays(3).Date.AddHours(20).AddHours(-48),
                Estado = "Confirmada",
                EventoVinculado = "Día del Amigo",
                EmpleadoCarga = "Carlos (Dueño)"
            }
        };
        SelectedReservation = AllReservations.FirstOrDefault();
    }

    partial void OnNewFechaChanged(DateTimeOffset value) => UpdateMaxCancellation();
    partial void OnNewHoraChanged(TimeSpan value) => UpdateMaxCancellation();

    private void UpdateMaxCancellation()
    {
        var targetDateTime = NewFecha.Date.Add(NewHora);
        ComputedMaxCancellation = targetDateTime.AddHours(-48);
    }

    partial void OnSearchFilterChanged(string value) => ApplyFilter();
    partial void OnStatusFilterChanged(string value) => ApplyFilter();

    private void ApplyFilter()
    {
        var query = AllReservations.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchFilter))
        {
            query = query.Where(r => r.ClienteNombre.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                                     r.ClienteDni.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase));
        }

        if (StatusFilter != "Todos")
        {
            query = query.Where(r => r.Estado == StatusFilter);
        }

        FilteredReservations = new ObservableCollection<ReservationModel>(query);
    }

    [RelayCommand]
    private void CreateReservation()
    {
        if (string.IsNullOrWhiteSpace(NewDni) || string.IsNullOrWhiteSpace(NewNombre))
        {
            FeedbackMessage = "❌ Error: DNI y Nombre del cliente son obligatorios.";
            HasFeedback = true;
            return;
        }

        var fullDate = NewFecha.Date.Add(NewHora);
        var newRes = new ReservationModel
        {
            Id = AllReservations.Max(r => r.Id) + 1,
            ClienteDni = NewDni,
            ClienteNombre = NewNombre,
            ClienteTelefono = NewTelefono,
            FechaHora = fullDate,
            CantidadPersonas = NewComensales,
            UbicacionSector = NewSector,
            MesasAsignadas = NewMesas,
            FechaMaxCancelacion = fullDate.AddHours(-48),
            Estado = "Confirmada",
            EventoVinculado = SelectedEvent == "Ninguno" ? null : SelectedEvent,
            EmpleadoCarga = "Usuario Activo"
        };

        AllReservations.Insert(0, newRes);
        ApplyFilter();

        FeedbackMessage = $"✅ Reserva #{newRes.Id} creada con éxito para {newRes.ClienteNombre}. Cancelable hasta: {newRes.CancelacionFormateada}";
        HasFeedback = true;

        // Reset form
        NewDni = string.Empty;
        NewNombre = string.Empty;
        NewTelefono = string.Empty;
    }

    [RelayCommand]
    private void ChangeStatus(string newStatus)
    {
        if (SelectedReservation == null) return;

        SelectedReservation.Estado = newStatus;
        FeedbackMessage = $"Reserva #{SelectedReservation.Id} actualizada a '{newStatus}'.";
        HasFeedback = true;
        ApplyFilter();
    }
}
