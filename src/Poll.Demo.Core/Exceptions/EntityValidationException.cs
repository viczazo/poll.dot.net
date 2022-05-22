using System;

namespace Poll.Demo.Core.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message) : base(message)
        {
        }
    }
}