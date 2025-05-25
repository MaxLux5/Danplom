using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.Services.Dialog.Base;
using Danplom.View.Windows;
using Danplom.ViewModel.Windows;
using System.Collections.ObjectModel;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestAdminListComponentViewModel : ObservableObject
{
    public ObservableCollection<RequestDto> Requests { get; } = new ObservableCollection<RequestDto>();

    public RequestAdminListComponentViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;
        //TODO тут должен быть запрос для получения полного списка запросов.
        Requests.Add(new RequestDto(-1, 100, 200, 0, "Да", -1));
    }

    [RelayCommand]
    private void AddRequest()
    {
        var viewModel = new CreateRequestWindowViewModel();

        viewModel.ResultDone += (obj, e) => Add(e.Value);

        dialogService.OpenDialog(new CreateRequestWindow(), viewModel);
    }

    [RelayCommand]
    private void RemoveRequest(RequestDto requestDto)
    {
        var viewModel = new NotifyWindowViewModel();
        viewModel.ResultDone += (obj, e) => Remove(requestDto);

        dialogService.OpenDialog(new NotifyWindow(), viewModel);
    }

    private readonly IDialogService dialogService;

    private void Add(RequestDto requestDto)
    {
        //TODO Тут должен быть запрос на добавление записи в БД.

        Requests.Add(requestDto);
    }

    private void Remove(RequestDto requestDto)
    {
        //TODO Тут должен быть запрос на удаление записи из БД.

        Requests.Remove(requestDto);
    }
}
