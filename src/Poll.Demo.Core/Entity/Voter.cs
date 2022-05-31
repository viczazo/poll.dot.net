using Poll.Demo.Core.Exceptions;

namespace Poll.Demo.Core.Entity
{
    public class Voter
    {
        public int Id { get; }
        public int NationalIdentity { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Voting Voting { get; }
        public Vote Vote { get; private set; }

        private Voter() { }

        public Voter(Voting voting, string firstName, string lastName, int nationalIdentity)
        {
            if (voting == null)
                throw new EntityValidationException("Voting not available");
            if (voting.State != VotingState.Idle)
                throw new EntityValidationException("Voting state must be Idle to add a voter");
            Voting = voting;
            if (string.IsNullOrWhiteSpace(firstName))
                throw new EntityValidationException("Voter first name must have value");
            FirstName = firstName;
            if (string.IsNullOrWhiteSpace(lastName))
                throw new EntityValidationException("Voter last name must have value");
            LastName = lastName;
            if (nationalIdentity <= 0)
                throw new EntityValidationException("Invalid nationality identity");
        }

        public void DoVote(VoteType voteType)
        {
            if (Vote != null)
            {
                throw new EntityValidationException("You have already vote, you can't change your vote");
            }
            Vote = new Vote(Voting, this, voteType);
        }
    }
}
