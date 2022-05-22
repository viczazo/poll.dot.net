using Poll.Demo.Application.Cqrs.Abstractions;

namespace Poll.Demo.Application.Cqrs.Command
{
    public class CreateVotingCommand : ICommand
    {
        public string Name { get; set; }
    }
}
