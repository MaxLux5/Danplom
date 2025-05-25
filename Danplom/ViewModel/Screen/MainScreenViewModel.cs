using CommunityToolkit.Mvvm.ComponentModel;
using Danplom.Model;
using Danplom.Services.Dialog;
using Danplom.Services.Dialog.Base;
using Danplom.View.Components.Requests;
using Danplom.ViewModel.Components.Requests;
using System.Windows.Controls;

namespace Danplom.ViewModel.Screen;

public partial class MainScreenViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _mainContent;

    public MainScreenViewModel(IDialogService dialogService, UserDto user)
    {
        ObservableObject viewModel;

        switch (user.Role)
        {
            case UserRole.Executor:
                MainContent = new RequestExecutorListComponent();
                viewModel = new RequestExecutorListComponentViewModel(dialogService);
                MainContent.DataContext = viewModel;
                break;
            case UserRole.Administrator:
                MainContent = new RequestAdminListComponent();
                viewModel = new RequestAdminListComponentViewModel(dialogService);
                MainContent.DataContext = viewModel;
                break;
        }
    }
}
