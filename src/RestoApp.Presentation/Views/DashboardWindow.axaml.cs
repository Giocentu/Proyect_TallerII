using Avalonia.Controls;
using Avalonia.Interactivity;
using RestoApp.Data.Models;

namespace RestoApp.Presentation.Views;

public partial class DashboardWindow : Window
{
    public DashboardWindow()
    {
        InitializeComponent();
    }

    public DashboardWindow(UsuarioSesion usuario)
    {
        InitializeComponent();

        txtBienvenida.Text = $"Hola, {usuario.Nombre} {usuario.Apellido}";
        txtRol.Text = $"Rol: {usuario.Rol}";

        ConfigurarPermisos(usuario);
    }

    private void ConfigurarPermisos(UsuarioSesion usuario)
    {
        if (!usuario.EsAdmin)
        {
            btnPersonal.IsVisible = false;
            btnCaja.IsVisible = false;
        }
    }

    public void BtnSalir_Click(object source, RoutedEventArgs args)
    {
        new MainWindow().Show();
        this.Close();
    }

    public void BtnPersonal_Click(object source, RoutedEventArgs args)
    {
        var ventanaPersonal = new PersonalWindow();
        ventanaPersonal.ShowDialog(this);
    }
}
