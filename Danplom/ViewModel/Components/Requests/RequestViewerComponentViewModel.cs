using CommunityToolkit.Mvvm.ComponentModel;
using Danplom.Model;

namespace Danplom.ViewModel.Components.Requests;

public partial class RequestViewerComponentViewModel : ObservableObject
{
    [ObservableProperty]
    private int _requiredQuantity;

    [ObservableProperty]
    private int _timeToComplete;

    [ObservableProperty]
    private string _detailId;

    [ObservableProperty]
    private int _workerId;

    public RequestViewerComponentViewModel(RequestDto request)
    {
        id = request.Id;
        RequiredQuantity = request.RequiredQuantity;
        TimeToComplete = request.TimeToComplete;
        requiredStatus = request.RequestStatus;
        DetailId = request.DetailId;
        WorkerId = request.WorkerId;
    }

    public RequestDto BuildRequest()
        => new RequestDto(id, RequiredQuantity, TimeToComplete, requiredStatus, DetailId, WorkerId);

    private readonly int requiredStatus;
    private readonly int id;
}
