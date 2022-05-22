using Poll.Demo.Application.Model;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class CreateVoteResponse
    {
        public AppActionResult<int> ActionResult { get; set; }

        public static CreateVoteResponse VoteResponseError(string errorMessage, ErrorType type)
        {
            return new CreateVoteResponse{ ActionResult = new AppActionResult<int>
            {
                ErrorMessage = errorMessage,
                ErrorType = type,
                IsSuccesfull = false
            }};
        }
    }
}