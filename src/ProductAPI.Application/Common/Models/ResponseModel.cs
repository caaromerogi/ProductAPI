namespace ProductAPI.Application.Common.Models;

public class ResponseModel<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public object? Errors { get; set; }

    public ResponseModel(T? data, string message, object? errors = null)
    {
        Data = data;
        Message = message;
        Errors = errors;
    }

}