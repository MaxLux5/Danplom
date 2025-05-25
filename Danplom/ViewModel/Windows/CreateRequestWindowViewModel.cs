using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.ViewModel.Components.Requests;
using Danplom.ViewModel.Dialog.Base;
using System.Windows;

namespace Danplom.ViewModel.Windows;

public partial class CreateRequestWindowViewModel : DefaultDialogViewModel<RequestDto>
{
    [ObservableProperty]
    public RequestViewerComponentViewModel _viewerViewModel = new RequestViewerComponentViewModel();

    [RelayCommand]
    private void Create()
    {
        Value = ViewerViewModel.BuildRequest();

        OkCommand.Execute(Value);
    }
}
