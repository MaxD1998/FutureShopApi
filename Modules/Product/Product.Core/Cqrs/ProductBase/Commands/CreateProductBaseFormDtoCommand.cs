using MediatR;
using Product.Core.Cqrs.ProductBase.Queries;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record CreateProductBaseFormDtoCommand(ProductBaseFormDto Dto) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class CreateProductBaseFormDtoCommandHanlder : IRequestHandler<CreateProductBaseFormDtoCommand, ResultDto<ProductBaseFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;

    public CreateProductBaseFormDtoCommandHanlder(ProductPostgreSqlContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(CreateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetProductBaseFormDtoByIdQuery(result.Entity.Id));
    }
}