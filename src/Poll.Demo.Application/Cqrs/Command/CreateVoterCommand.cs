using Poll.Demo.Application.Cqrs.Abstractions;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class CreateVoterCommand : ICommand
    {
        public int VotingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NationalityId { get; set; }
    }
}