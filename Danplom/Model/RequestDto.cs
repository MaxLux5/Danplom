namespace Danplom.Model;

public record RequestDto(int Id, int RequiredQuantity, int TimeToComplete, int RequestStatus, string DetailId, int WorkerId);
