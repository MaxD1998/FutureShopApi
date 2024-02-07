﻿using Shared.Domain.Bases;

namespace Authorization.Domain.Entities;

public class RefreshTokenEntity : BaseEntity
{
    public DateOnly EndDate { get; set; }

    public DateOnly StartDate { get; set; }

    public Guid Token { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public UserEntity User { get; set; }

    #endregion Related Data
}