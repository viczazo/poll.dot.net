namespace Poll.Demo.Integration.Tests.Models;

public class AddVoteDto
{
    public int VotingId { get; set; }
    public int VoterId { get; set; }
    public byte Type { get; set; }
}

public enum AddVoteType: byte
{
    Negative,
    Positive
}