using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Cqrs.Category.Queries;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.Category.Commands;
public record UpdateCategoryFormDtoCommand(Guid Id, CategoryFormDto Dto) : IRequest<ResultDto<CategoryFormDto>>;

internal class UpdateCategoryFormDtoCommandHandler : BaseService, IRequestHandler<UpdateCategoryFormDtoCommand, ResultDto<CategoryFormDto>>
{
    private readonly ShopContext _context;
    private readonly IMediator _mediator;

    public UpdateCategoryFormDtoCommandHandler(ShopContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<CategoryFormDto>> Handle(UpdateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<CategoryFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetCategoryFormDtoByIdQuery(request.Id), cancellationToken);
    }
}