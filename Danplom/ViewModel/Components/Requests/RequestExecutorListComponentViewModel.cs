using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.Services.Dialog.Base;
using Danplom.View.Windows;
using Danplom.ViewModel.Windows;
using Danplom.DataBaseConnector;
using System.Collections.ObjectModel;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestExecutorListComponentViewModel : ObservableObject
{
    public ObservableCollection<RequestDto> Requests { get; } = new ObservableCollection<RequestDto>();

    public RequestExecutorListComponentViewModel(IDialogService dialogService, UserDto user)
    {
        this.dialogService = dialogService;

        var collection = DataBase.GetInstance().GetCurrentExecutorRequestCollection(user);
        if (collection is null) return;

        Requests.Clear();
        foreach (var item in collection)
        {
            Requests.Add(item);
        }
    }

    [RelayCommand]
    public void CompleteRequest(RequestDto request)
    {
        var viewModel = new NotifyWindowViewModel();
        viewModel.ResultDone += (obj, e) => Complete(request);

        dialogService.OpenDialog(new NotifyWindow(), viewModel);
    }

    private readonly IDialogService dialogService;

    private void Complete(RequestDto request)
    {
        request.Status = RequestStatus.Completed;
        if (!DataBase.GetInstance().ChangeRequest(request)) return;

        Requests.Remove(request);
    }
}
