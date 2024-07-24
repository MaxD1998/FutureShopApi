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
        //var a = System.IO.File.ReadAllBytes("C:\\Users\\MaksMichalski\\Desktop\\ala.exe");

        //if (a.Length < 16 * 1000 * 1024)
        //{
        //    await _context.AddAsync<ProductPhotoEntity>(new()
        //    {
        //        Data = [],
        //        Name = "name",
        //    });
        //}

        //var bucket = new GridFSBucket(_context.Database, new()
        //{
        //    BucketName = "ProductPhoto",
        //    ChunkSizeBytes = 5 * 1000 * 1024 //5MB
        //});
        //await bucket.UploadFromBytesAsync("Test.exe", a);

        await _context.AddAsync<ProductPhotoEntity>(new()
        {
            Name = "name",
        });

        return Ok();
    }
}