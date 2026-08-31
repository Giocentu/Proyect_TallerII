using Avalonia.Controls;
using RestoApp.Presentation.ViewModels;

namespace RestoApp.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
