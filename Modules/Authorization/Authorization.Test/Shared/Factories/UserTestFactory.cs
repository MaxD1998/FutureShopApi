using Authorization.Domain.Aggregates.Users;

namespace Authorization.Test.Shared.Factories;

internal static class UserTestFactory
{
    public static User Create()
    {
        var id = Guid.NewGuid();
        var dateOfBirth = new DateOnly(1900, 1, 1);
        var email = "test@futureshop.pl";
        var hashedPassword = "HashedPassword";
        var firstName = "Test";
        var lastName = "User";

        var user = new User(dateOfBirth, email, hashedPassword, firstName, lastName);

        user.GetType().GetProperty(nameof(User.Id)).SetValue(user, id);

        return user;
    }
}