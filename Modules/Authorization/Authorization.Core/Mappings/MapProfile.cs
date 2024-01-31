using Authorization.Core.Dtos.RefreshToken;
using Authorization.Domain.Entities;
using AutoMapper;
using FluentValidation.Results;
using Shared.Core.Dtos;

namespace Authorization.Core.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        #region Entity to Entity

        CreateMap<RefreshTokenEntity, RefreshTokenEntity>();
        CreateMap<UserEntity, UserEntity>();

        #endregion Entity to Entity

        #region Dto to Entity

        CreateMap<RefreshTokenInputDto, RefreshTokenEntity>();

        #endregion Dto to Entity

        #region Dto to Dto

        CreateMap<ValidationFailure, ErrorDto>();

        #endregion Dto to Dto
    }
}