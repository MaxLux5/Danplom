using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.Services.Dialog;
using Danplom.Services.Dialog.Base;
using Danplom.View.Windows;
using Danplom.ViewModel.Windows;
using System.Collections.ObjectModel;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestExecutorListComponentViewModel : ObservableObject
{
    public ObservableCollection<RequestDto> Requests { get; } = new ObservableCollection<RequestDto>();

    public RequestExecutorListComponentViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;

        //TODO тут должен быть запрос для получения нужного списка запросов.

        Requests.Add(new RequestDto(-1, 100, 200, 0, "Да", -1));
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
        //TODO тут должен быть запрос на внесения изменения

        Requests.Remove(request);
    }
}
