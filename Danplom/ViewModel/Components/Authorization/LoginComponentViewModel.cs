using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.ViewModel.Components.Authorization.Events;

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
        //TODO Тут должен быть запрос в БД для авторизации

        if (true)
        {
            //TODO Тут должен быть запрос в БД для получения подробной информации о пользователе

            OnLoginCompleted(this, new LoginEventArgs(new UserDto(-1, "Тестовый", UserRole.Administrator, "Да", "Да")));
        }
    }
    private void OnLoginCompleted(object? sender, LoginEventArgs e)
    {
        LoginCompleted?.Invoke(sender, e);
    }
}
