using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Application.Commands.Product.DeleteProduct;
using ProductAPI.Application.Commands.Product.UpdateProduct;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;
using ProductAPI.Application.Queries.Product.GetProductById;

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

    [HttpGet("GetProductsPagination")]
    public async Task<IActionResult> GetPaginatedResult([FromQuery] GetPaginatedProductQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("GetProductById")]
    public async Task<IActionResult> GetProductById([FromQuery] GetProductByIdQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
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
    public async Task<IActionResult> Delete([FromQuery] DeleteProductCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}