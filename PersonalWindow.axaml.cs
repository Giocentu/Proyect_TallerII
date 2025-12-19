using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MiProyecto;

public partial class PersonalWindow : Window
{
    public PersonalWindow()
    {
        InitializeComponent();
    }

    public void BtnVolver_Click(object source, RoutedEventArgs args)
    {
        this.Close(); // Solo cerramos esta ventana para volver al Dashboard que está abajo
    }
}