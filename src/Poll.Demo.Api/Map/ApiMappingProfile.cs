using AutoMapper;
using Poll.Demo.Api.Models;
using Poll.Demo.Application.Cqrs.Command;

namespace Poll.Demo.Api.Map
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateVotingDto, CreateVotingCommand>();
            CreateMap<CreateVoterDto, CreateVoterCommand>();
            CreateMap<AddVoteDto, CreateVoteCommand>();
            CreateMap(typeof(Application.Model.AppActionResult<>), typeof(WebApiActionResult<>));
            CreateMap(typeof(Application.Model.AppActionResult), typeof(WebApiActionResult));
        }
    }
}