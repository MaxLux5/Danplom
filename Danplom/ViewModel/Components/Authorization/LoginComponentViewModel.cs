using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.ViewModel.Components.Authorization.Events;
using Danplom.DataBaseConnector;

namespace Danplom.ViewModel.Components.Authorization;

public partial class LoginComponentViewModel : ObservableObject
{
    [ObservableProperty]
    private string _login;
    [ObservableProperty]
    private string _password;

    /// <summary>
    /// Событие, вызывающееся при успешной авторизации.
    /// </summary>
    public event LoginEventHandler LoginCompleted;


    [RelayCommand]
    private void TryLogin()
    {
        var loginedUser = DataBase.GetInstance().Login(Login, Password);
        if (loginedUser is null) return;

        OnLoginCompleted(this, new LoginEventArgs(loginedUser));
    }
    private void OnLoginCompleted(object? sender, LoginEventArgs e)
    {
        LoginCompleted?.Invoke(sender, e);
    }
}
