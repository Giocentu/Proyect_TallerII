using Avalonia.Controls;
using Avalonia.Interactivity;
using MiProyecto.Models; // Necesario para usar UsuarioSesion

namespace MiProyecto;

public partial class DashboardWindow : Window
{
    // Constructor vacío (lo usa Avalonia para dibujar la vista previa)
    public DashboardWindow()
    {
        InitializeComponent();
    }

    // Constructor PRINCIPAL (Este se ejecuta cuando alguien se loguea)
    public DashboardWindow(UsuarioSesion usuario)
    {
        InitializeComponent();

        // 1. Llenamos los textos de bienvenida
        txtBienvenida.Text = $"Hola, {usuario.Nombre} {usuario.Apellido}";
        txtRol.Text = $"Rol: {usuario.Rol}";

        // 2. Configuramos qué botones puede ver según si es Admin o no
        ConfigurarPermisos(usuario);
    }

    private void ConfigurarPermisos(UsuarioSesion usuario)
    {
        // Si NO es admin (EsAdmin == false), ocultamos botones sensibles
        if (!usuario.EsAdmin)
        {
            btnPersonal.IsVisible = false; // Un mozo no contrata gente
            btnCaja.IsVisible = false;     // Un mozo no ve el balance financiero
        }
        
        // Si ES admin, no hacemos nada (por defecto todo es visible)
    }

    public void BtnSalir_Click(object source, RoutedEventArgs args)
    {
        // Abrimos de nuevo el Login y cerramos esta ventana
        new MainWindow().Show();
        this.Close();
    }

    public void BtnPersonal_Click(object source, RoutedEventArgs args)
    {
        var ventanaPersonal = new PersonalWindow();
        ventanaPersonal.ShowDialog(this); 
        // ShowDialog bloquea el dashboard hasta que cierres la ventana de personal.
        // Es útil para que no abran 50 ventanas a la vez.
    }
}