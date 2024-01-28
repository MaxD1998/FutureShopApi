﻿using Core.Interfaces;

namespace Core.Dtos.RefreshToken;

public class RefreshTokenInputDto : IDto
{
    public DateOnly EndDate { get; set; }

    public DateOnly StartDate { get; set; }

    public Guid Token { get; set; }

    public Guid UserId { get; set; }
}