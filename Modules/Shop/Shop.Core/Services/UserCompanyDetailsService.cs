using Shared.Core.Dtos;
using Shop.Core.Dtos.User.UserCompanyDetails;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IUserCompanyDetailsService
{
    Task<ResultDto<UserCompanyDetailsResponseFormDto>> CreateAsync(UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<UserCompanyDetailsResponseFormDto>> GetByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<ResultDto<List<UserCompanyDetailsResponseFormDto>>> GetListByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<ResultDto<UserCompanyDetailsResponseFormDto>> UpdateAsync(Guid id, UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken);
}

internal class UserCompanyDetailsService(IUserCompanyDetailsRepository userCompanyDetailsRepository, IUserRepository userRepository) : IUserCompanyDetailsService
{
    private readonly IUserCompanyDetailsRepository _userCompanyDetailsRepository = userCompanyDetailsRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResultDto<UserCompanyDetailsResponseFormDto>> CreateAsync(UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetIdByExternalIdAsync(dto.UserExternalId, cancellationToken);
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

    public async Task<ResultDto<UserCompanyDetailsResponseFormDto>> GetByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
    {
        var result = await _userCompanyDetailsRepository.GetByExternalIdAsync(externalId, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<UserCompanyDetailsResponseFormDto>>> GetListByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
    {
        var results = await _userCompanyDetailsRepository.GetListAsync(x => x.User.ExternalId == externalId, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<UserCompanyDetailsResponseFormDto>> UpdateAsync(Guid id, UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetIdByExternalIdAsync(dto.UserExternalId, cancellationToken);
        var entity = dto.ToEntity();

        entity.UserId = userId;
        entity = await _userCompanyDetailsRepository.UpdateAsync(id, entity, cancellationToken);

        var result = await _userCompanyDetailsRepository.GetByIdAsync(entity.Id, UserCompanyDetailsResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}