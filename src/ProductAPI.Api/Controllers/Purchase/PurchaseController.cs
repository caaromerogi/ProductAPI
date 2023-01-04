using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Commands.Purchase.CreatePurchase;

namespace ProductAPI.Api.Controllers.Purchase;
[Route("[Controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IMediator _mediator;
    public PurchaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreatePurchaseOrder")]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}