namespace Poll.Demo.Api.Models;

public class CreateVoterDto
{
    public int VotingId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int NationalityId { get; set; }
}