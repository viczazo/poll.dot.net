using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class ChangeVotingStateCommand : ICommand
    {
        public int VotingId { get; set; }
        public VotingState VotingState { get; set; }
    }
}
