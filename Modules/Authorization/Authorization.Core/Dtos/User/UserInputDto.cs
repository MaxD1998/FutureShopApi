using Shared.Core.Interfaces;

namespace Authorization.Core.Dtos.User;

public class UserInputDto : IDto
{
    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }
}