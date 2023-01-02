namespace ProductAPI.Domain.Exceptions.Abstract;

public abstract class ExceptionModel : Exception
{
    protected object? Errors { get; set; }

    public ExceptionModel(
        string message, 
        object? errors = null) 
        : base(message)
    {
        Errors = errors;
    }

}