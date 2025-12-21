using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Interfaces.Services;
using Shop.Core.Dtos.User.UserDeliveryAddress;
using Shop.Core.Interfaces.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IUserDeliveryAddressService
{
    Task<ResultDto<UserDeliveryAddressResponseFormDto>> CreateAsync(UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<UserDeliveryAddressResponseFormDto>>> GetListAsync(CancellationToken cancellationToken);

    Task<ResultDto<UserDeliveryAddressResponseFormDto>> UpdateAsync(Guid id, UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken);
}

public class UserDeliveryAddressService(ICurrentUserService currentUserService, IUserDeliveryAddressRepository userDeliveryAddressRepository, IUserRepository userRepository) : IUserDeliveryAddressService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IUserDeliveryAddressRepository _userDeliveryAddressRepository = userDeliveryAddressRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResultDto<UserDeliveryAddressResponseFormDto>> CreateAsync(UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken)
    {
        var nullableUserId = _currentUserService.GetUserId();

        if (!nullableUserId.HasValue)
            return ResultDto.Error<UserDeliveryAddressResponseFormDto>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var userId = nullableUserId.Value;

        if (dto.IsDefault)
        {
            var otherRecordIsDefault = await _userDeliveryAddressRepository.AnyIsDefaultByUserExternalIdAsync(userId, cancellationToken);

            if (otherRecordIsDefault)
                await _userDeliveryAddressRepository.ClearIsDefaultByUserExternalIdAsync(userId, cancellationToken);
        }

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

    public async Task<ResultDto<List<UserDeliveryAddressResponseFormDto>>> GetListAsync(CancellationToken cancellationToken)
    {
        var nullableId = _currentUserService.GetUserId();

        if (!nullableId.HasValue)
            return ResultDto.Error<List<UserDeliveryAddressResponseFormDto>>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var id = nullableId.Value;
        var results = await _userDeliveryAddressRepository.GetListAsync(x => x.User.ExternalId == id, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<UserDeliveryAddressResponseFormDto>> UpdateAsync(Guid id, UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken)
    {
        var nullableUserId = _currentUserService.GetUserId();

        if (!nullableUserId.HasValue)
            return ResultDto.Error<UserDeliveryAddressResponseFormDto>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var userId = nullableUserId.Value;

        if (dto.IsDefault)
        {
            var otherRecordIsDefault = await _userDeliveryAddressRepository.AnyIsDefaultByUserExternalIdAsync(userId, cancellationToken);

            if (otherRecordIsDefault)
                await _userDeliveryAddressRepository.ClearIsDefaultByUserExternalIdAsync(userId, cancellationToken);
        }

        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userDeliveryAddressRepository.UpdateAsync(id, entity, cancellationToken);

        var result = await _userDeliveryAddressRepository.GetByIdAsync(entity.Id, UserDeliveryAddressResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}