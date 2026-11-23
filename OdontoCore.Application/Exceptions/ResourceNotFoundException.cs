using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Exceptions
{
    public sealed class ResourceNotFoundException: Exception
    {
        public ResourceNotFoundException(string resourceName): base($"{resourceName} was not found or is inactive.")
        {
        }
    }
}
