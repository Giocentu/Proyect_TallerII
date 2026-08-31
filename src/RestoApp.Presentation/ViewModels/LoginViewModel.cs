using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly Action<UserModel> _onLoginSuccess;

    [ObservableProperty]
    private string _dni = "12345678";

    [ObservableProperty]
    private string _password = "admin";

    [ObservableProperty]
    private string _selectedRole = "Dueño";

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError = false;

    public string[] AvailableRoles => new[] { "Dueño", "Gerente", "Recepcionista", "Cajero", "Mozo" };

    public LoginViewModel(Action<UserModel> onLoginSuccess)
    {
        _onLoginSuccess = onLoginSuccess;
    }

    [RelayCommand]
    private void Login()
    {
        if (string.IsNullOrWhiteSpace(Dni))
        {
            ErrorMessage = "Por favor, ingrese un DNI válido.";
            HasError = true;
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Por favor, ingrese la contraseña.";
            HasError = true;
            return;
        }

        HasError = false;
        ErrorMessage = string.Empty;

        // Mock User creation based on input and selected role
        var user = new UserModel
        {
            Dni = Dni,
            Nombre = SelectedRole switch
            {
                "Dueño" => "Carlos",
                "Gerente" => "Laura",
                "Recepcionista" => "Sofía",
                "Cajero" => "Martín",
                _ => "Mozo Juan"
            },
            Apellido = "Gómez",
            Rol = SelectedRole,
            ActivoEnRol = true
        };

        _onLoginSuccess(user);
    }

    [RelayCommand]
    private void QuickSelectRole(string role)
    {
        SelectedRole = role;
        Dni = role switch
        {
            "Dueño" => "11111111",
            "Gerente" => "22222222",
            "Recepcionista" => "33333333",
            "Cajero" => "44444444",
            _ => "55555555"
        };
        Password = "password123";
        Login();
    }
}
