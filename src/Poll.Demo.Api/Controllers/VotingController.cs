using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Poll.Demo.Api.Models;
using Poll.Demo.Api.Models.Extensions;
using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Application.Cqrs.Command;
using Poll.Demo.Application.Cqrs.Query;
using Poll.Demo.Application.Model;
using Poll.Demo.Application.Model.Voting;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VotingController : ControllerBase
{
    private readonly IMapper _mapper;
    public VotingController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(WebApiActionResult<int>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.InternalServerError)]
    public async Task<ObjectResult> Create([FromServices] IRequestHandler<CreateVotingCommand, AppActionResult<int>> handler, 
        CreateVotingDto voting, CancellationToken cancellationToken)
    {
        var votingCommand = _mapper.Map<CreateVotingCommand>(voting);
        var actionResult = await handler.Handle(votingCommand, cancellationToken);
        var result = _mapper.Map<WebApiActionResult<int>>(actionResult);
        return result.ReturnWebResult();
    }

    [HttpPost("{id:int}/open")]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ObjectResult> Open([FromServices] IRequestHandler<ChangeVotingStateCommand, AppActionResult> handler,
        int id, CancellationToken cancellationToken)
    {
        var actionResult = await handler.Handle(
            new ChangeVotingStateCommand{ VotingId = id, VotingState = VotingState.Opened },
            cancellationToken);
        var result = _mapper.Map<WebApiActionResult>(actionResult);
        return result.ReturnWebResult();
    }
    
    [HttpPost("{id:int}/close")]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(WebApiActionResult), (int)HttpStatusCode.InternalServerError)]
    public async Task<ObjectResult> Close([FromServices] IRequestHandler<ChangeVotingStateCommand, AppActionResult> handler,
        int id, CancellationToken cancellationToken)
    {
        var actionResult = await handler.Handle(
            new ChangeVotingStateCommand{ VotingId = id, VotingState = VotingState.Closed },
            cancellationToken);
        var result = _mapper.Map<WebApiActionResult>(actionResult);
        return result.ReturnWebResult();
    }

    [HttpGet("{id:int}/results")]
    public async Task<ObjectResult> GetResults([FromServices] IRequestHandler<GetVotingResults, AppActionResult<VotingResults>> handler,
        int id, CancellationToken cancellationToken)
    {
        var actionResult = await handler.Handle(
            new GetVotingResults{VotingId = id}, cancellationToken);
        var result = _mapper.Map<WebApiActionResult<VotingResults>>(actionResult);
        return result.ReturnWebResult();
    }
}