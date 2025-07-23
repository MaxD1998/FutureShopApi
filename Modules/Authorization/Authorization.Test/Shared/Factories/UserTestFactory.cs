using Authorization.Domain.Aggregates.Users;
using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;

namespace Authorization.Test.Shared.Factories;

internal static class UserTestFactory
{
    public static UserAggregate Create()
    {
        var id = Guid.NewGuid();
        var dateOfBirth = new DateOnly(1900, 1, 1);
        var email = "test@futureshop.pl";
        var hashedPassword = "HashedPassword";
        var firstName = "Test";
        var lastName = "User";

        var user = new UserAggregate(dateOfBirth, email, hashedPassword, firstName, lastName);

        user.GetType().GetProperty(nameof(UserAggregate.Id)).SetValue(user, id);

        return user;
    }

    public static RefreshTokenEntity CreateRefreshToken(int expireTime)
    {
        var id = Guid.NewGuid();
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        var endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(expireTime));
        var token = Guid.NewGuid();
        var refreshToken = new RefreshTokenEntity(startDate, endDate, token);

        refreshToken.GetType().GetProperty(nameof(RefreshTokenEntity.Id)).SetValue(refreshToken, id);

        return refreshToken;
    }
}