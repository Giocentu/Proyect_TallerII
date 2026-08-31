using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private UserModel _currentUser;

    [ObservableProperty]
    private ViewModelBase _currentView;

    [ObservableProperty]
    private string _activeTabName = "Mesas";

    [ObservableProperty]
    private string _globalNotification = string.Empty;

    [ObservableProperty]
    private bool _hasNotification = false;

    public string[] DemoRoles => new[] { "Dueño", "Gerente", "Recepcionista", "Cajero", "Mozo" };

    // Sub-ViewModels
    public TablesViewModel TablesVM { get; } = new();
    public ReservationsViewModel ReservationsVM { get; } = new();
    public StaffViewModel StaffVM { get; } = new();
    public CashierViewModel CashierVM { get; } = new();
    public EventsViewModel EventsVM { get; } = new();

    private readonly System.Action _onLogout;

    public MainViewModel(UserModel user, System.Action onLogout)
    {
        _currentUser = user;
        _onLogout = onLogout;
        _currentView = TablesVM;
        
        UpdateStaffContext();
    }

    [RelayCommand]
    private void Navigate(string destination)
    {
        ActiveTabName = destination;
        CurrentView = destination switch
        {
            "Mesas" => TablesVM,
            "Reservas" => ReservationsVM,
            "Personal" => StaffVM,
            "Caja" => CashierVM,
            "Eventos" => EventsVM,
            _ => TablesVM
        };
    }

    [RelayCommand]
    private void ChangeDemoRole(string newRole)
    {
        CurrentUser.Rol = newRole;
        OnPropertyChanged(nameof(CurrentUser));
        UpdateStaffContext();

        GlobalNotification = $"🔄 Rol de usuario simulado cambiado a: {newRole}. Módulos y permisos RBAC actualizados.";
        HasNotification = true;
    }

    private void UpdateStaffContext()
    {
        StaffVM.CurrentUserRole = CurrentUser.Rol;
    }

    [RelayCommand]
    private void Logout()
    {
        _onLogout();
    }

    [RelayCommand]
    private void DismissNotification()
    {
        HasNotification = false;
    }
}
