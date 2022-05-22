using Poll.Demo.Application.Model;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class CreateVoterResponse
    {
        public AppActionResult<int> ActionResult { get; set; }

        public static CreateVoterResponse VoteResponseError(string errorMessage, ErrorType type)
        {
            return new CreateVoterResponse{ ActionResult = new AppActionResult<int>
            {
                ErrorMessage = errorMessage,
                ErrorType = type,
                IsSuccesfull = false
            }};
        }
    }
}