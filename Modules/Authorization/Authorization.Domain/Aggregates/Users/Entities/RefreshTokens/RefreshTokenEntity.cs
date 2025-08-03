using Authorization.Domain.Aggregates.Users.Entities.RefreshTokens.Exceptions;
using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Interfaces;

namespace Authorization.Domain.Aggregates.Users.Entities.RefreshTokens;

public class RefreshTokenEntity : BaseEntity, ICloneable<RefreshTokenEntity>, IUpdate<RefreshTokenEntity>
{
    public RefreshTokenEntity()
    {
    }

    public RefreshTokenEntity(DateOnly startDate, DateOnly endDate, Guid token)
    {
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetToken(token);
    }

    public DateOnly EndDate { get; private set; }

    public DateOnly StartDate { get; private set; }

    public Guid Token { get; private set; }

    public Guid UserId { get; private set; }

    #region Setters

    private void SetEndDate(DateOnly endDate)
    {
        if (endDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new RefreshTokenEndDateInPastException();

        EndDate = endDate;
    }

    private void SetStartDate(DateOnly startDate)
    {
        if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new RefreshTokenStartDateInFutureException();

        StartDate = startDate;
    }

    private void SetToken(Guid token)
    {
        ValidateRequiredProperty(nameof(Token), token);

        Token = token;
    }

    #endregion Setters

    #region Methods

    public RefreshTokenEntity Clone() => new()
    {
        Id = Id,
        CreateTime = CreateTime,
        ModifyTime = ModifyTime,
        EndDate = EndDate,
        StartDate = StartDate,
        Token = Token,
        UserId = UserId,
    };

    public void Update(RefreshTokenEntity entity)
    {
        if (EndDate != entity.EndDate)
            EndDate = entity.EndDate;

        if (StartDate != entity.StartDate)
            StartDate = entity.StartDate;

        if (Token != entity.Token)
            Token = entity.Token;
    }

    #endregion Methods
}