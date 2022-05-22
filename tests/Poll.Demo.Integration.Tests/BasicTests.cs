using Poll.Demo.Integration.Tests.Models;
using Refit;
using Xunit;

namespace Poll.Demo.Integration.Tests
{
    public class BasicTests
    {
        private readonly IApiClient _apiClient;
        public BasicTests()
        {
            _apiClient = RestService.For<IApiClient>("http://localhost:5228/api");
        }
        
        [Fact]
        public async Task All_voting_Flows()
        {
            var voters = 13; 
            var voterIds = new List<int>();
            short positiveVotes = 0;
            short negativeVotes = 0;
            
            //Create voting
            var response = await _apiClient.CreateVoting(new CreateVoting{ Name = "Voting" });
            Assert.Equal(200, (int)response.StatusCode);
            Assert.True(response.Content.Data > 0);
            var votingId = response.Content.Data;
            
            //Add some voters 
            for (var i = 0; i < voters; i++)
            {
                var vote = await _apiClient.AddVoter(new CreateVoter
                    { FirstName = "Some", LastName = "Voter", VotingId = votingId, NationalityId = i});
                
                voterIds.Add(vote.Content!.Data);
            }
            
            //Open voting
            var openResult = await _apiClient.OpenVoting(votingId);
            Assert.True(openResult.IsSuccessStatusCode);
            
            //Vote
            for (var i = 0; i < voters; i++)
            {
                var positiveVote = voterIds[i] % 2 == 0;
                _ = (positiveVote) ? positiveVotes++ : negativeVotes++;
                var voteResponse = await _apiClient.AddVote(new AddVoteDto
                    { VotingId = votingId, VoterId = voterIds[i], Type = Convert.ToByte(positiveVote) });
                Assert.True(voteResponse.IsSuccessStatusCode);
                Assert.True(voteResponse.Content.Data > 0);
            }
            
            //Close voting
            var closeVotingResult = await _apiClient.CloseVoting(votingId);
            Assert.True(closeVotingResult.IsSuccessStatusCode);
            
            //Get Results
            var votingResult = await _apiClient.GetResults(votingId);
            Assert.True(votingResult.IsSuccessStatusCode);
            Assert.Equal(positiveVotes, votingResult.Content.Data.Positives );
            Assert.Equal(negativeVotes, votingResult.Content.Data.Negatives );
        }
    }
}
