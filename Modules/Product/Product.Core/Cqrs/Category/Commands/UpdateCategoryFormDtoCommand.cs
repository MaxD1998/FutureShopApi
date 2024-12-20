using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Cqrs.Category.Queries;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Errors;
using Shared.Infrastructure;
using System.Net;

namespace Product.Core.Cqrs.Category.Commands;
public record UpdateCategoryFormDtoCommand(Guid Id, CategoryFormDto Dto) : IRequest<ResultDto<CategoryFormDto>>;

internal class UpdateCategoryFormDtoCommandHandler : BaseService, IRequestHandler<UpdateCategoryFormDtoCommand, ResultDto<CategoryFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;
    private readonly RabbitMqContext _rabbitMqContext;

    public UpdateCategoryFormDtoCommandHandler(ProductPostgreSqlContext context, IMediator mediator, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _mediator = mediator;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto<CategoryFormDto>> Handle(UpdateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<CategoryFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity(_context));

        await _context.SaveChangesAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        return await _mediator.Send(new GetCategoryFormDtoByIdQuery(request.Id));
    }
}