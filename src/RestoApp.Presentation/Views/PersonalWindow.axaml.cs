using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RestoApp.Presentation.Views;

public partial class PersonalWindow : Window
{
    public PersonalWindow()
    {
        InitializeComponent();
    }

    public void BtnVolver_Click(object source, RoutedEventArgs args)
    {
        this.Close();
    }
}
