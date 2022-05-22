using System.Text.Json.Serialization;

namespace Poll.Demo.Api.Models;


public class WebApiActionResult
{
    [JsonIgnore]
    public bool IsSuccesfull { get; set; }
    public string ErrorMessage { get; set; }
    [JsonIgnore]
    public WebErrorType ErrorType { get; set; }
}

public class WebApiActionResult<T> : WebApiActionResult
{
    public T Data { get; set; }
}

public enum WebErrorType
{
    General,
    Validation
}