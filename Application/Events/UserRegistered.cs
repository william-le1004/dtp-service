namespace Application.Events;

public record UserRegistered
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
}