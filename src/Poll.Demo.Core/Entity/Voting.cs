using System;
using System.Collections.Generic;
using Poll.Demo.Core.Exceptions;

namespace Poll.Demo.Core.Entity
{
    public class Voting
    {
        public int Id { get; }
        public string Name { get; }
        public VotingState State { get; private set; }
        public List<Voter> Voters { get; } = new List<Voter>();

        private Voting() { }

        public Voting(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EntityValidationException("Voting name must have value");
            Name = name;
        }

        public void ChangeState(VotingState state)
        {
            if (State == VotingState.Closed)
                throw new EntityValidationException("Voting ended");
            if(State == VotingState.Opened && state == VotingState.Idle)
                throw new EntityValidationException("When voting is open can't go to Idle");
            State = state;
        }
    }
}
