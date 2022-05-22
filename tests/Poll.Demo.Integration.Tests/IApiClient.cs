using Poll.Demo.Integration.Tests.Models;
using Refit;

namespace Poll.Demo.Integration.Tests;

public interface IApiClient
{
    [Post("/voting")]
    public Task<ApiResponse<WebApiResult<int>>> CreateVoting(CreateVoting voting);

    [Post("/voting/{id}/open")]
    public Task<ApiResponse<WebApiResult>> OpenVoting(int id);
    
    [Post("/voting/{id}/close")]
    public Task<ApiResponse<WebApiResult>> CloseVoting(int id);

    [Get("/voting/{id}/results")]
    public Task<ApiResponse<WebApiResult<VotingResults>>> GetResults(int id);
    
    [Post("/voter")]
    public Task<ApiResponse<WebApiResult<int>>> AddVoter(CreateVoter createVoter);

    [Post("/vote")]
    public Task<ApiResponse<WebApiResult<int>>> AddVote(AddVoteDto createVoterDto);

}