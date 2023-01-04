using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using ProductAPI.Domain.Error;
using ProductAPI.Domain.Exceptions.Implementation;

namespace ProductAPI.Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch(ex)
            {
                case ProductNotFoundException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var errorModelNotFound = new ErrorModelBuilder()
                    .WithErrorCode("01")
                    .WithMessage(ex.Message)
                    .Build();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModelNotFound));
                    break;

                case ValidationException exception:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var errorsDictionary = new Dictionary<string,string>();
                    foreach (var item in exception.Errors)
                    {
                        errorsDictionary.Add(item.PropertyName, item.ErrorMessage);
                    }
                    var errorModelValidationError = new ErrorModelBuilder()
                    .WithErrorCode("02")
                    .WithMessage(ex.Message)
                    .WithAdditionalInf(errorsDictionary)
                    .Build();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModelValidationError));
                    break;

            }
        }
    }
}