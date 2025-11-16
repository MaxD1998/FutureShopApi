using Shared.Core.Dtos;
using Shop.Core.Dtos.User.UserDeliveryAddress;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IUserDeliveryAddressService
{
    Task<ResultDto<UserDeliveryAddressResponseFormDto>> CreateAsync(UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<UserDeliveryAddressResponseFormDto>> GetByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<ResultDto<List<UserDeliveryAddressResponseFormDto>>> GetListByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<ResultDto<UserDeliveryAddressResponseFormDto>> UpdateAsync(Guid id, UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);
}

public class UserDeliveryAddressService(IUserDeliveryAddressRepository userDeliveryAddressRepository, IUserRepository userRepository) : IUserDeliveryAddressService
{
    private readonly IUserDeliveryAddressRepository _userDeliveryAddressRepository = userDeliveryAddressRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResultDto<UserDeliveryAddressResponseFormDto>> CreateAsync(UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetIdByExternalIdAsync(dto.UserExternalId, cancellationToken);
        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userDeliveryAddressRepository.CreateAsync(entity, cancellationToken);

        var result = await _userDeliveryAddressRepository.GetByIdAsync(entity.Id, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userDeliveryAddressRepository.DeleteByIdAsync(id, cancellationToken);
        return ResultDto.Success();
    }

    public async Task<ResultDto<UserDeliveryAddressResponseFormDto>> GetByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
    {
        var result = await _userDeliveryAddressRepository.GetByExternalIdAsync(externalId, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<UserDeliveryAddressResponseFormDto>>> GetListByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
    {
        var results = await _userDeliveryAddressRepository.GetListAsync(x => x.User.ExternalId == externalId, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<UserDeliveryAddressResponseFormDto>> UpdateAsync(Guid id, UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetIdByExternalIdAsync(dto.UserExternalId, cancellationToken);
        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userDeliveryAddressRepository.UpdateAsync(id, entity, cancellationToken);

        var result = await _userDeliveryAddressRepository.GetByIdAsync(entity.Id, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}