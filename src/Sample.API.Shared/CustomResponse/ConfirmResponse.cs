using System.Net;

namespace Sample.API.Shared.CustomResponse;

public class ConfirmResponse
{
    public HttpStatusCode StatusCode { get; }
    public bool Success { get; set; }
    public object Message { get; set; }

    public ConfirmResponse()
    {
        StatusCode = HttpStatusCode.OK;
        Success = true;
    }

    public ConfirmResponse(object message)
    {
        StatusCode = HttpStatusCode.OK;
        Success = true;
        Message = message;
    }

    public ConfirmResponse(HttpStatusCode statusCode, bool success)
    {
        StatusCode = statusCode;
        Success = success;
    }

    public ConfirmResponse(HttpStatusCode statusCode, bool success, object message)
    {
        StatusCode = statusCode;
        Success = success;
        Message = message;
    }
}
