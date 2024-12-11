using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Cqrs.ProductBase.Queries;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using System.Net;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record UpdateProductBaseFormDtoCommand(Guid Id, ProductBaseFormDto Dto) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class UpdateProductBaseFormDtoCommandHandler : BaseService, IRequestHandler<UpdateProductBaseFormDtoCommand, ResultDto<ProductBaseFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;

    public UpdateProductBaseFormDtoCommandHandler(ProductPostgreSqlContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(UpdateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<ProductBaseFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetProductBaseFormDtoByIdQuery(request.Id));
    }
}