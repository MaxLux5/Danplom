using CommunityToolkit.Mvvm.ComponentModel;
using Danplom.DataBaseConnector;
using Danplom.Model;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Windows;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestViewerComponentViewModel : ObservableObject
{
    [ObservableProperty]
    private DetailDto _currentDetail;
    [ObservableProperty]
    private int _requiredQuantity;
    [ObservableProperty]
    private int _timeToComplete;
    [ObservableProperty]
    private UserDto _currentExecutor;
    [ObservableProperty]
    private RequestStatus _currentStatus;


    public ObservableCollection<DetailDto> Details { get; set; } = new ObservableCollection<DetailDto>();
    public ObservableCollection<UserDto> Executors { get; set; } = new ObservableCollection<UserDto>();


    public RequestViewerComponentViewModel()
    {
        var collection = DataBase.GetInstance().GetDetailCollection();
        if (collection is null) return;

        Details.Clear();
        foreach (var item in collection)
        {
            Details.Add(item);
        }

        var executorCollection = DataBase.GetInstance().GetExecutorCollection();
        if (executorCollection is null) return;

        Executors.Clear();
        foreach (var item in executorCollection)
        {
            Executors.Add(item);
        }
    }

    public RequestDto BuildRequest()
    {
        if (CurrentDetail is null) CurrentDetail = Details[0];
        if (CurrentExecutor is null) CurrentExecutor = Executors[0];
        if (CurrentStatus != RequestStatus.InProgress ||
            CurrentStatus != RequestStatus.PartiallyCompleted ||
            CurrentStatus != RequestStatus.Completed ||
            CurrentStatus != RequestStatus.NotCompleted)
        {
            CurrentStatus = RequestStatus.NotCompleted;
        }

        return new RequestDto(-1, CurrentDetail.Id, CurrentDetail.Name, RequiredQuantity, TimeToComplete, CurrentExecutor.Id,
            CurrentExecutor.Name, CurrentExecutor.Role, CurrentExecutor.Login, CurrentExecutor.Password, CurrentStatus);
    }
}
