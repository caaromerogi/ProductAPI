using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Commands.Purchase.CreatePurchase;
using ProductAPI.Application.Queries.Purchase.GetPurchase;

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

    [HttpGet("GetPurchases")]
    public async Task<IActionResult> Get([FromQuery] GetPurchaseQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}