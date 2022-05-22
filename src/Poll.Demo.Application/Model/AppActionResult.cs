namespace Poll.Demo.Application.Model
{
    public class AppActionResult
    {
        public bool IsSuccesfull { get; set; } = true;
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; } = ErrorType.General;
        
        public static AppActionResult CreateError(string errorMessage, ErrorType type)
        {
            return new AppActionResult
            {
                IsSuccesfull = false,
                ErrorMessage = errorMessage,
                ErrorType = type
            };
        }
    }

    public class AppActionResult<T> : AppActionResult
    {
        public T Data { get; set; }
        
        public new static AppActionResult<T> CreateError(string errorMessage, ErrorType type)
        {
            return new AppActionResult<T>
            {
                IsSuccesfull = false,
                ErrorMessage = errorMessage,
                ErrorType = type,
                Data = default(T)
            };
        }
    }

    public enum ErrorType
    {
        General,
        Validation
    }
}
