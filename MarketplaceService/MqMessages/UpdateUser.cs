using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplaceservice.MqMessages
{
    public class UpdateUserMessage
    {
        public Guid Id { get; set; }
        public string NewEmail { get; set; }
    }
}
