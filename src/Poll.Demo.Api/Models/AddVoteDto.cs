
namespace Poll.Demo.Api.Models;

public class AddVoteDto
{
    public int VotingId { get; set; }
    public int VoterId { get; set; }
    public AddVoteType Type { get; set; }
}

public enum AddVoteType: byte
{
    Negative,
    Positive
}