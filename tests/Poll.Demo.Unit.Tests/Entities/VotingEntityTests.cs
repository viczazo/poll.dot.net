using Poll.Demo.Core.Entity;
using Poll.Demo.Core.Exceptions;
using Xunit;

namespace Poll.Demo.Unit.Tests.Entities;

public class VotingEntityTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    public void Create_Voting_Not_Valid_Name_ThrowsException(string name)
    {
        Assert.Throws<EntityValidationException>(() => new Voting(name));
    }
    
    [Fact]
    public void Create_Voting_Valid_Name_Success()
    {
        Assert.NotNull(new Voting("Some name"));
    }
    
    [Theory]
    [InlineData(VotingState.Closed, VotingState.Opened)]
    [InlineData(VotingState.Closed, VotingState.Idle)]
    [InlineData(VotingState.Opened, VotingState.Idle)]
    public void Voting_Change_State_From_To_ThrowsException(VotingState from, VotingState to)
    {
        var voting = new Voting("Some name");
        voting.ChangeState(from);
        Assert.Throws<EntityValidationException>(() => voting.ChangeState(to));
    }
}