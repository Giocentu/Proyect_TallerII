using Avalonia.Controls;
using Avalonia.Interactivity;
using MiProyecto.Data;
using MiProyecto.Models;

namespace MiProyecto;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent(); // Dibuja la ventana y los botones
    }

    // Este código se ejecuta cuando el usuario hace clic en "Ingresar"
    public void BtnLogin_Click(object source, RoutedEventArgs args)
    {
        // 1. VALIDACIÓN VISUAL:
        // Antes de nada, revisamos si las cajas de texto están vacías.
        if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
        {
            lblStatus.Text = "⚠️ Ingrese usuario y contraseña";
            lblStatus.Foreground = Avalonia.Media.Brushes.Orange;
            return; // Cortamos aquí. No seguimos.
        }

        // 2. LLAMADA A LA BASE DE DATOS:
        // Creamos una instancia de nuestra clase Database
        var db = new Database();
        
        // Llamamos al método Login pasándole lo que escribió el usuario.
        // Esto ejecutará todo el código del Archivo 2.
        var usuario = db.Login(txtUser.Text, txtPass.Text);

        // 3. VERIFICAR RESULTADO:
        if (usuario != null)
        {
            // SI NO ES NULL: Significa que Login() encontró al usuario y nos devolvió sus datos.
            lblStatus.Text = $"✅ ¡Hola {usuario.Nombre}! ({usuario.Rol})";
            lblStatus.Foreground = Avalonia.Media.Brushes.Green;
            
            // (Aquí luego pondremos el código para cambiar de ventana)
        }
        else
        {
            // SI ES NULL: Puede ser por 3 razones:
            // a) Escribió mal el DNI (letras).
            // b) Escribió mal la contraseña.
            // c) El usuario existe pero 'activo_en_rol' es 0 en la base de datos.
            lblStatus.Text = "❌ Datos incorrectos o usuario inactivo";
            lblStatus.Foreground = Avalonia.Media.Brushes.Red;
        }
    }
}