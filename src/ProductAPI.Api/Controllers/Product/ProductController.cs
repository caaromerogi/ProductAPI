using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Application.Commands.Product.DeleteProduct;
using ProductAPI.Application.Commands.Product.UpdateProduct;

namespace ProductAPI.Api.Controllers.Product;
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> Add([FromBody] CreateProductCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }

    [HttpPut("EditProduct")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}