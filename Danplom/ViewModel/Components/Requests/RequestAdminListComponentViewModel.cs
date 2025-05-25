using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Model;
using Danplom.Services.Dialog.Base;
using Danplom.View.Windows;
using Danplom.ViewModel.Windows;
using Danplom.DataBaseConnector;
using System.Collections.ObjectModel;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestAdminListComponentViewModel : ObservableObject
{
    public ObservableCollection<RequestDto> Requests { get; } = new ObservableCollection<RequestDto>();

    public RequestAdminListComponentViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;

        LoadData();
    }

    private void LoadData()
    {
        var collection = DataBase.GetInstance().GetRequestCollection();
        if (collection is null) return;

        Requests.Clear();
        foreach (var item in collection)
        {
            Requests.Add(item);
        }
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
        if(!DataBase.GetInstance().AddNewRequest(requestDto)) return;

        LoadData();
    }

    private void Remove(RequestDto requestDto)
    {
        if (!DataBase.GetInstance().DeleteRequest(requestDto)) return;

        Requests.Remove(requestDto);
    }
}
