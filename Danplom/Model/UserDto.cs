namespace Danplom.Model;

public record UserDto(int Id, string Name, UserRole Role, string Login, string Password);
