using Microsoft.AspNetCore.Mvc;

namespace Poll.Demo.Api.Models.Extensions;

public static class ActionResultExtensions
{
    public static ObjectResult ReturnWebResult(this WebApiActionResult result)
    {
        return GetResult(result);
    }

    public static ObjectResult ReturnWebResult<T>(this WebApiActionResult<T> result)
    {
        return GetResult(result);
    }

    private static ObjectResult GetResult(WebApiActionResult result)
    {
        return result.IsSuccesfull switch
        {
            true => new ObjectResult(result) { StatusCode = 200 },
            false when result.ErrorType == WebErrorType.Validation => new ObjectResult(result) { StatusCode = 400 },
            _ => new ObjectResult(result) { StatusCode = 500 }
        };
    }
}