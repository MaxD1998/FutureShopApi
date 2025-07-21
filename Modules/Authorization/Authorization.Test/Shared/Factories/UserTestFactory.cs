using Authorization.Domain.Aggregates.Users;
using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;

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

    public static RefreshToken CreateRefreshToken(int expireTime)
    {
        var id = Guid.NewGuid();
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        var endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(expireTime));
        var token = Guid.NewGuid();
        var refreshToken = new RefreshToken(startDate, endDate, token);

        refreshToken.GetType().GetProperty(nameof(RefreshToken.Id)).SetValue(refreshToken, id);

        return refreshToken;
    }
}