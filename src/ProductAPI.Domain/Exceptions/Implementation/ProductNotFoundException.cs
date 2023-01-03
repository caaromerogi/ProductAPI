using ProductAPI.Domain.Exceptions.Abstract;

namespace ProductAPI.Domain.Exceptions.Implementation;

public class ProductNotFoundException : ExceptionModel
{
    public ProductNotFoundException(string message, object? errors = null) : base(message, errors)
    {
    }
}