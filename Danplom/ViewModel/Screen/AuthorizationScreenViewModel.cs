using CommunityToolkit.Mvvm.ComponentModel;
using Danplom.ViewModel.Components.Authorization;
using Danplom.ViewModel.Components.Authorization.Events;

namespace Danplom.ViewModel.Screen;

public partial class AuthorizationScreenViewModel : ObservableObject
{
    [ObservableProperty]
    private LoginComponentViewModel _loginComponentViewModel;

    public AuthorizationScreenViewModel()
    {
        LoginComponentViewModel = new LoginComponentViewModel();
        LoginComponentViewModel.LoginCompleted += OnLoginCompleted;
    }

    /// <summary>
    /// Промежуточное событие, вызывающееся при успешной авторизации.
    /// </summary>
    public event LoginEventHandler LoginCompleted;
    private void OnLoginCompleted(object? sender, LoginEventArgs e)
    {
        LoginCompleted?.Invoke(sender, e);
    }
}
