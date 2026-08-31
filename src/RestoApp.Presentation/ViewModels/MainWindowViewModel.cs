using CommunityToolkit.Mvvm.ComponentModel;
using RestoApp.Presentation.Models;

namespace RestoApp.Presentation.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        _currentViewModel = CreateLoginViewModel();
    }

    private LoginViewModel CreateLoginViewModel()
    {
        return new LoginViewModel(OnLoginSuccess);
    }

    private void OnLoginSuccess(UserModel user)
    {
        CurrentViewModel = new MainViewModel(user, OnLogout);
    }

    private void OnLogout()
    {
        CurrentViewModel = CreateLoginViewModel();
    }
}
