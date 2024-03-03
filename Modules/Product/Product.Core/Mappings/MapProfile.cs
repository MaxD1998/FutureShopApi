using AutoMapper;
using FluentValidation.Results;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.CategoryTranslation;
using Product.Domain.Entities;
using Shared.Core.Dtos;

namespace Product.Core.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        #region Entity to Entity

        CreateMap<CategoryEntity, CategoryEntity>();
        CreateMap<ProductBaseEntity, ProductBaseEntity>();
        CreateMap<ProductEntity, ProductEntity>();
        CreateMap<ProductParameterEntity, ProductParameterEntity>();
        CreateMap<ProductParameterValueEntity, ProductParameterValueEntity>();

        #endregion Entity to Entity

        #region Dto to Entity

        CreateMap<CategoryInputDto, CategoryEntity>();
        CreateMap<CategoryTranslationInputDto, CategoryTranslationEntity>();

        #endregion Dto to Entity

        #region Entity to Dto

        CreateMap<CategoryEntity, CategoryDto>()
            .ForMember(dest => dest.HasChildren, cfg => cfg.MapFrom(src => src.SubCategories.Any()));

        #endregion Entity to Dto

        #region Dto to Dto

        CreateMap<ValidationFailure, ErrorDto>();

        #endregion Dto to Dto
    }
}