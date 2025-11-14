using Authorization.Core.Dtos.User;
using Authorization.Core.Errors;
using Authorization.Infrastructure.Repositories;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Services;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using System.Net;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Services;

public interface IUserService
{
    Task<ResultDto<UserResponseFormDto>> CreateAsync(UserCreateRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto> DeleteOwnAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<UserResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<UserListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<UserResponseFormDto>> UpdateAsync(Guid id, UserUpdateRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> UpdateOwnPasswordAsync(Guid id, UserPasswordFormDto dto, CancellationToken cancellationToken);
}

internal class UserService(ICurrentUserService currentUserService, IRabbitMqContext rabbitMqContext, IUserRepository userRepository) : IUserService
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResultDto<UserResponseFormDto>> CreateAsync(UserCreateRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.CreateAsync(dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.AuthorizationModuleUser, entity);

        var result = await _userRepository.GetByIdAsync(entity.Id, UserResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteByIdAsync(id, cancellationToken);
        return ResultDto.Success();
    }

    public Task<ResultDto> DeleteOwnAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsRecordOwner(id))
            return Task.FromResult(ResultDto.Error(HttpStatusCode.Forbidden, CommonExceptionMessage.C005UserIsNotTheOwnerOfThisRecord));

        return DeleteAsync(id, cancellationToken);
    }

    public async Task<ResultDto<UserResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetByIdAsync(id, UserResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<PageDto<UserListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _userRepository.GetPageAsync(pagination, UserListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<UserResponseFormDto>> UpdateAsync(Guid id, UserUpdateRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();
        entity = await _userRepository.UpdateAsync(id, entity, cancellationToken);

        if (entity is null)
            return ResultDto.Error<UserResponseFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C004RecordWasNotFound);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.AuthorizationModuleUser, entity);

        var result = await _userRepository.GetByIdAsync(entity.Id, UserResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> UpdateOwnPasswordAsync(Guid id, UserPasswordFormDto dto, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsRecordOwner(id))
            return ResultDto.Error(HttpStatusCode.Forbidden, CommonExceptionMessage.C005UserIsNotTheOwnerOfThisRecord);

        var hashedPassword = await _userRepository.GetByIdAsync(id, x => x.HashedPassword, cancellationToken);

        if (hashedPassword is null)
            return ResultDto.Error(HttpStatusCode.NotFound, CommonExceptionMessage.C004RecordWasNotFound);

        if (!Crypt.Verify(dto.OldPassword, hashedPassword))
            return ResultDto.Error(HttpStatusCode.BadRequest, ExceptionMessage.User003OldPasswordWasWrong);

        hashedPassword = Crypt.HashPassword(dto.NewPassword);

        await _userRepository.UpdatePasswordAsync(id, hashedPassword, cancellationToken);

        return ResultDto.Success();
    }
}