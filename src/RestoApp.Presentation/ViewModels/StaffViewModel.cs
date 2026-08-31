using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class StaffViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<StaffModel> _allStaff = new();

    [ObservableProperty]
    private ObservableCollection<StaffModel> _filteredStaff = new();

    [ObservableProperty]
    private StaffModel? _selectedStaff;

    // Form fields
    [ObservableProperty]
    private string _newDni = string.Empty;

    [ObservableProperty]
    private string _newNombre = string.Empty;

    [ObservableProperty]
    private string _newApellido = string.Empty;

    [ObservableProperty]
    private string _newEmail = string.Empty;

    [ObservableProperty]
    private string _newTelefono = string.Empty;

    [ObservableProperty]
    private string _newRol = "Mozo";

    [ObservableProperty]
    private string _newShiftStart = "08:00";

    [ObservableProperty]
    private string _newShiftEnd = "16:00";

    [ObservableProperty]
    private string _selectedRoleFilter = "Todos";

    [ObservableProperty]
    private string _currentUserRole = "Dueño";

    [ObservableProperty]
    private string _feedbackMessage = string.Empty;

    [ObservableProperty]
    private bool _hasFeedback = false;

    public string[] Roles => new[] { "Dueño", "Gerente", "Recepcionista", "Cajero", "Mozo" };
    public string[] RoleFilters => new[] { "Todos", "Dueño", "Gerente", "Recepcionista", "Cajero", "Mozo" };

    public StaffViewModel()
    {
        LoadMockStaff();
        ApplyFilter();
    }

    private void LoadMockStaff()
    {
        AllStaff = new ObservableCollection<StaffModel>
        {
            new StaffModel { Dni = "11111111", Nombre = "Carlos", Apellido = "Gómez", Email = "carlos@resto.com", Telefono = "11-2222-3333", Rol = "Dueño", TurnoInicio = "08:00", TurnoFin = "23:59", ActivoEnRol = true },
            new StaffModel { Dni = "22222222", Nombre = "Laura", Apellido = "Fernández", Email = "laura@resto.com", Telefono = "11-4444-5555", Rol = "Gerente", TurnoInicio = "12:00", TurnoFin = "20:00", ActivoEnRol = true },
            new StaffModel { Dni = "33333333", Nombre = "Sofía", Apellido = "Rodríguez", Email = "sofia@resto.com", Telefono = "11-6666-7777", Rol = "Recepcionista", TurnoInicio = "18:00", TurnoFin = "01:00", ActivoEnRol = true },
            new StaffModel { Dni = "44444444", Nombre = "Martín", Apellido = "López", Email = "martin@resto.com", Telefono = "11-8888-9999", Rol = "Cajero", TurnoInicio = "16:00", TurnoFin = "00:00", ActivoEnRol = true },
            new StaffModel { Dni = "55555555", Nombre = "Juan", Apellido = "Pérez", Email = "juan@resto.com", Telefono = "11-1234-5678", Rol = "Mozo", TurnoInicio = "19:00", TurnoFin = "02:00", ActivoEnRol = true }
        };
        SelectedStaff = AllStaff.FirstOrDefault();
    }

    partial void OnSelectedRoleFilterChanged(string value) => ApplyFilter();

    private void ApplyFilter()
    {
        if (SelectedRoleFilter == "Todos")
        {
            FilteredStaff = new ObservableCollection<StaffModel>(AllStaff);
        }
        else
        {
            FilteredStaff = new ObservableCollection<StaffModel>(AllStaff.Where(s => s.Rol == SelectedRoleFilter));
        }
    }

    [RelayCommand]
    private void RegisterStaff()
    {
        if (string.IsNullOrWhiteSpace(NewDni) || string.IsNullOrWhiteSpace(NewNombre) || string.IsNullOrWhiteSpace(NewApellido))
        {
            FeedbackMessage = "❌ Error: DNI, Nombre y Apellido son requeridos.";
            HasFeedback = true;
            return;
        }

        // RN-03: Rule check for role creation hierarchy
        if (CurrentUserRole == "Gerente" && (NewRol == "Dueño" || NewRol == "Gerente"))
        {
            FeedbackMessage = "⚠️ Restricción RN-03: Un Gerente no puede dar de alta usuarios con rol Dueño o Gerente.";
            HasFeedback = true;
            return;
        }

        if (AllStaff.Any(s => s.Dni == NewDni))
        {
            FeedbackMessage = "❌ Error: Ya existe un empleado registrado con el DNI ingresado.";
            HasFeedback = true;
            return;
        }

        var newEmployee = new StaffModel
        {
            Dni = NewDni,
            Nombre = NewNombre,
            Apellido = NewApellido,
            Email = NewEmail,
            Telefono = NewTelefono,
            Rol = NewRol,
            TurnoInicio = NewShiftStart,
            TurnoFin = NewShiftEnd,
            ActivoEnRol = true
        };

        AllStaff.Add(newEmployee);
        ApplyFilter();

        FeedbackMessage = $"✅ Empleado {newEmployee.NombreCompleto} ({newEmployee.Rol}) dado de alta exitosamente.";
        HasFeedback = true;

        NewDni = string.Empty;
        NewNombre = string.Empty;
        NewApellido = string.Empty;
        NewEmail = string.Empty;
        NewTelefono = string.Empty;
    }

    [RelayCommand]
    private void ToggleActive(StaffModel staff)
    {
        staff.ActivoEnRol = !staff.ActivoEnRol;
        FeedbackMessage = $"Estado de {staff.NombreCompleto} actualizado a: {(staff.ActivoEnRol ? "Activo" : "Inactivo")}.";
        HasFeedback = true;
    }
}
