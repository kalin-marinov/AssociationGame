using System;
using System.Collections.Generic;

namespace Game.Core.Exceptions
{

    public class InputValidationException : Exception
    {
        public IEnumerable<string> Errors { get; private set; }

        public InputValidationException(IEnumerable<string> errors) : base(message: string.Join("; ", errors))
        {
            this.Errors = errors;
        }

    }
}
