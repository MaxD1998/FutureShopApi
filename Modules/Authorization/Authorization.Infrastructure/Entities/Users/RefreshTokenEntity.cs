using Authorization.Infrastructure.Exceptions.RefreshTokens;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Entities.Users;

public class RefreshTokenEntity : BaseEntity, IUpdate<RefreshTokenEntity>, IEntityValidation
{
    public DateOnly EndDate { get; set; }

    public DateOnly StartDate { get; set; }

    public Guid Token { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data

    #region Methods

    public void Update(RefreshTokenEntity entity)
    {
        EndDate = entity.EndDate;
        StartDate = entity.StartDate;
        Token = entity.Token;
        UserId = entity.UserId;
    }

    public void Validate()
    {
        var utcNow = DateOnly.FromDateTime(DateTime.UtcNow);
        ValidateEndDate(utcNow);
        ValidateStartDate(utcNow);
        ValidateToken();
    }

    private void ValidateEndDate(DateOnly date)
    {
        if (EndDate < date)
            throw new RefreshTokenEndDateInPastException();
    }

    private void ValidateStartDate(DateOnly date)
    {
        if (StartDate > date)
            throw new RefreshTokenStartDateInFutureException();
    }

    private void ValidateToken()
    {
        if (Token == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(Token));
    }

    #endregion Methods
}