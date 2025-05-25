using Danplom.Model;

namespace Danplom.ViewModel.Components.Authorization.Events;

public class LoginEventArgs : EventArgs
{
    public UserDto User { get; }
    public LoginEventArgs(UserDto user)
    {
        User = user;
    }
}
