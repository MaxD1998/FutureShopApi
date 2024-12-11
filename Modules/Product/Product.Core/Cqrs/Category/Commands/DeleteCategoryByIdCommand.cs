using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Category.Commands;
public record DeleteCategoryByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteCategoryByIdCommandHandler : BaseService, IRequestHandler<DeleteCategoryByIdCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteCategoryByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<CategoryEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Success();
    }
}