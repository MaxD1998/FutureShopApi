using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;
using Shared.Infrastructure;

namespace Api.Modules.Shop.Controllers;

public class TestController : BaseController
{
    private readonly RabbitMqContext _rabbitMq;

    public TestController(RabbitMqContext rabbitMq, IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
        _rabbitMq = rabbitMq;
    }

    [HttpPost]
    public void Test()
    {
        _ = _rabbitMq.SendMessageAsync("Test", "Murzyb");
    }
}