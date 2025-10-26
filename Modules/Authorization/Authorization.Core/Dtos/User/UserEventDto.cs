﻿using Authorization.Infrastructure.Entities;

namespace Authorization.Core.Dtos.User;

public class UserEventDto(UserEntity entity)
{
    public string FirstName => entity.FirstName;

    public Guid Id => entity.Id;

    public string LastName => entity.LastName;
}