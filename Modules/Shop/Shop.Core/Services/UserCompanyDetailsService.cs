using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shop.Core.Dtos.User.UserCompanyDetails;
using Shop.Infrastructure.Repositories;
using System.Net;

namespace Shop.Core.Services;

public interface IUserCompanyDetailsService
{
    Task<ResultDto<UserCompanyDetailsResponseFormDto>> CreateAsync(UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<UserCompanyDetailsResponseFormDto>>> GetListAsync(CancellationToken cancellationToken);

    Task<ResultDto<UserCompanyDetailsResponseFormDto>> UpdateAsync(Guid id, UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);
}

internal class UserCompanyDetailsService(ICurrentUserService currentUserService, IUserCompanyDetailsRepository userCompanyDetailsRepository) : IUserCompanyDetailsService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IUserCompanyDetailsRepository _userCompanyDetailsRepository = userCompanyDetailsRepository;

    public async Task<ResultDto<UserCompanyDetailsResponseFormDto>> CreateAsync(UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken)
    {
        var nullableUserId = _currentUserService.GetUserId();

        if (!nullableUserId.HasValue)
            return ResultDto.Error<UserCompanyDetailsResponseFormDto>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var userId = nullableUserId.Value;
        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userCompanyDetailsRepository.CreateAsync(entity, cancellationToken);

        var result = await _userCompanyDetailsRepository.GetByIdAsync(entity.Id, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userCompanyDetailsRepository.DeleteByIdAsync(id, cancellationToken);
        return ResultDto.Success();
    }

    public async Task<ResultDto<List<UserCompanyDetailsResponseFormDto>>> GetListAsync(CancellationToken cancellationToken)
    {
        var nullableId = _currentUserService.GetUserId();

        if (!nullableId.HasValue)
            return ResultDto.Error<List<UserCompanyDetailsResponseFormDto>>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var id = nullableId.Value;
        var results = await _userCompanyDetailsRepository.GetListAsync(x => x.User.ExternalId == id, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<UserCompanyDetailsResponseFormDto>> UpdateAsync(Guid id, UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken)
    {
        var nullableUserId = _currentUserService.GetUserId();

        if (!nullableUserId.HasValue)
            return ResultDto.Error<UserCompanyDetailsResponseFormDto>(HttpStatusCode.Unauthorized, CommonExceptionMessage.C005YouMustBeLoggedInToPerformThisAction);

        var userId = nullableUserId.Value;
        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userCompanyDetailsRepository.UpdateAsync(id, entity, cancellationToken);

        var result = await _userCompanyDetailsRepository.GetByIdAsync(entity.Id, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}