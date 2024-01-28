using AutoMapper;
using Core.Dtos.RefreshToken;
using Domain.Entities;
using FluentValidation.Results;
using Shared.Bases;
using Shared.Dtos;

namespace Core.Mappings;

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