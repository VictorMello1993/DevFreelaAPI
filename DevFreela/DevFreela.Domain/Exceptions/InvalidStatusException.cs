using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException(string entity) : base($"{entity} has an invalid status.")
        {

        }
    }
}
