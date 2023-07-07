using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Shared.CustomResponse.Common;

public class CustomDetails : ProblemDetails
{
    public int? TypeCode { get; set; }
}