using CommunityToolkit.Mvvm.ComponentModel;

namespace Danplom.Model
{
    public partial class RequestDto : ObservableObject
    {
        public int Id { get; private set; }
        [ObservableProperty]
        private DetailDto _detail;
        [ObservableProperty]
        private int _requiredQuantity;
        [ObservableProperty]
        private int _timeToComplete;
        [ObservableProperty]
        private UserDto _user;
        [ObservableProperty]
        private RequestStatus _status;


        public RequestDto(int id, string detailId, string detailName, int requiredQuantity, int timeToComplete,
            int userId, string userName, UserRole role, string login, string password, RequestStatus status)
        {
            Id = id;
            Detail = new DetailDto(detailId, detailName);
            RequiredQuantity = requiredQuantity;
            TimeToComplete = timeToComplete;
            User = new UserDto(userId, userName, role, login, password);
            Status = status;
        }
    }
}