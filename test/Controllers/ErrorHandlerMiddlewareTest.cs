using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ProductAPI.Api.Middleware;
using ProductAPI.Domain.Exceptions.Implementation;

namespace test.Controllers;

public class ErrorHandlerMiddlewareTest{
    
    [Fact]
    public async Task ProductNotFound_404()
    {
        //Arrange 
        HttpContext ctx = new DefaultHttpContext();
        RequestDelegate next = (HttpContext hc) => throw new ProductNotFoundException("");
        ErrorHandlerMiddleware mw = new ErrorHandlerMiddleware(next);

        //Act
        await mw.Invoke(ctx);

        //Assert
        ctx.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task ValidationFailure_400(){
        //Arrange 
        HttpContext ctx = new DefaultHttpContext();
        RequestDelegate next = (HttpContext hc) => throw new ValidationException("");
        ErrorHandlerMiddleware mw = new ErrorHandlerMiddleware(next);

        //Act
        await mw.Invoke(ctx);

        //Assert
        ctx.Response.StatusCode.Should().Be(400);
    }
}