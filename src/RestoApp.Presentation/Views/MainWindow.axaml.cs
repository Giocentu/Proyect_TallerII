using Avalonia.Controls;
using Avalonia.Interactivity;
using RestoApp.Data;
using RestoApp.Data.Models;

namespace RestoApp.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void BtnLogin_Click(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
        {
            lblStatus.Text = "⚠️ Ingrese usuario y contraseña";
            lblStatus.Foreground = Avalonia.Media.Brushes.Orange;
            return;
        }

        var db = new Database();
        var usuario = db.Login(txtUser.Text, txtPass.Text);

        if (usuario != null)
        {
            lblStatus.Text = "¡Acceso Correcto!";
            lblStatus.Foreground = Avalonia.Media.Brushes.Green;

            var dashboard = new DashboardWindow(usuario);
            dashboard.Show();
            this.Close();
        }
        else
        {
            lblStatus.Text = "❌ Datos incorrectos o usuario inactivo";
            lblStatus.Foreground = Avalonia.Media.Brushes.Red;
        }
    }
}
