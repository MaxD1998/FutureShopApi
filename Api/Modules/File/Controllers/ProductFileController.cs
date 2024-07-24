using File.Domain.Entities;
using File.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.File.Controllers;

public class ProductFileController : BaseController
{
    private readonly FileContext _context;

    public ProductFileController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator, FileContext context) : base(fluentValidatorFactory, mediator)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> A()
    {
        await _context.AddAsync<ProductPhotoEntity>(new()
        {
            Name = "name",
        });

        return Ok();
    }
}