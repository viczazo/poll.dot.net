namespace Poll.Demo.Application.Cqrs.Query
{
    public class GetVotingResults : IQuery
    {
        public int VotingId { get; set; }
    }
}