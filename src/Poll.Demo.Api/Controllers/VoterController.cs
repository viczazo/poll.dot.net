using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Poll.Demo.Api.Models;
using Poll.Demo.Api.Models.Extensions;
using Poll.Demo.Application.Cqrs.Command;

namespace Poll.Demo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoterController : ControllerBase
{
    private readonly IMapper _mapper;

    public VoterController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ObjectResult> Add([FromServices] IRequestClient<CreateVoterCommand> client,
        CreateVoterDto createVoterDto, CancellationToken cancellationToken)
    {
        var cmd = _mapper.Map<CreateVoterCommand>(createVoterDto);
        var actionResult = await client.GetResponse<CreateVoterResponse>(cmd, cancellationToken);
        var result = _mapper.Map<WebApiActionResult<int>>(actionResult.Message.ActionResult);
        return result.ReturnWebResult();
    }
}