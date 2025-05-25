using CommunityToolkit.Mvvm.ComponentModel;
using Danplom.Services.Dialog;
using Danplom.Services.Dialog.Base;
using Danplom.View.Screen;
using Danplom.ViewModel.Components.Authorization.Events;
using Danplom.ViewModel.Screen;
using System.Windows.Controls;

namespace Danplom.ViewModel.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _currentScreen;
    
    private IDialogService dialogService = new DialogService();


    public MainWindowViewModel()
    {
        CurrentScreen = new AuthorizationScreen();
        var authViewModel = new AuthorizationScreenViewModel();
        CurrentScreen.DataContext = authViewModel;
        authViewModel.LoginCompleted += Login;
    }


    /// <summary>
    /// Смена страницы.
    /// </summary>
    private void Login(object? sender, LoginEventArgs e)
    {
        CurrentScreen = new MainScreen();
        CurrentScreen.DataContext = new MainScreenViewModel(dialogService, e.User);
    }
}
