using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class CreateVoteCommand : ICommand 
    {
        public int VotingId { get; set; }
        public int VoterId { get; set; }
        public VoteType Type { get; set; }
    }
}