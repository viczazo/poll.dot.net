namespace Poll.Demo.Integration.Tests.Models;

public class WebApiResult
{
    public bool IsSuccesfull { get; set; }
    public string ErrorMessage { get; set; }
}

public class WebApiResult<T> : WebApiResult
{
    public T Data { get; set; }
}
