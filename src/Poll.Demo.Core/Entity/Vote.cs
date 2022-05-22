using System;
using Poll.Demo.Core.Exceptions;

namespace Poll.Demo.Core.Entity
{
    public class Vote
    {
        public int Id { get; }
        public Voting Voting { get; }

        public Voter Voter { get; }

        public VoteType Type { get; }

        private Vote() { }
        public Vote(Voting voting, Voter voter, VoteType type)
        {
            if (voting.State != VotingState.Opened)
                throw new EntityValidationException("Voting is not opened, you can't vote");
            Voting = voting; 
            Voter = voter ?? throw new EntityValidationException("You can't vote with non existent voter");
            Type = type;
        }
    }
}
