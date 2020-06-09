using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplaceservice.Exceptions
{
    public class NotAuthorisedException : Exception
    {
        public NotAuthorisedException() : base("You are not authorised to do this") { }
    }
}
