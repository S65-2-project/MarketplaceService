using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplaceservice.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base("You are not authenticated to do this") { }
    }
}
