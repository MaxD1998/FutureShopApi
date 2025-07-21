using Authorization.Domain.Aggregates.Users.Entities;

namespace Authorization.Core.Dtos.RefreshTokens;

public class RefreshTokenFormDto
{
    public DateOnly EndDate { get; set; }

    public DateOnly StartDate { get; set; }

    public Guid Token { get; set; }

    public Guid UserId { get; set; }

    public RefreshTokenEntity ToEntity() => new()
    {
        EndDate = EndDate,
        StartDate = StartDate,
        Token = Token,
        UserId = UserId
    };
}