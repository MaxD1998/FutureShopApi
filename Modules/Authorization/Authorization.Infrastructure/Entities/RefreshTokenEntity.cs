using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Entities;

public class RefreshTokenEntity : BaseEntity, IUpdate<RefreshTokenEntity>
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

    #endregion Methods
}