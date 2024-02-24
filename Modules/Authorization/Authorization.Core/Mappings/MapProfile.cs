using Authorization.Core.Dtos.RefreshToken;
using Authorization.Core.Dtos.User;
using Authorization.Domain.Entities;
using AutoMapper;
using FluentValidation.Results;
using Shared.Core.Dtos;
using Crypt = BCrypt.Net.BCrypt;

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
        CreateMap<UserInputDto, UserEntity>()
            .ForMember(dest => dest.DateOfBirth, cfg => cfg.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)))
            .ForMember(dest => dest.HashedPassword, cfg => cfg.MapFrom(src => Crypt.HashPassword(src.Password)));

        #endregion Dto to Entity

        #region Dto to Dto

        CreateMap<ValidationFailure, ErrorDto>();

        #endregion Dto to Dto
    }
}