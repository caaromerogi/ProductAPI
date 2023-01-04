namespace ProductAPI.Application.Common.Models;

public class ResponseModel
{

    public string Message { get; set; }

    public ResponseModel(string message)
    {
        Message = message;
    }

}