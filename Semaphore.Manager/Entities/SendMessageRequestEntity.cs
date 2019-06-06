using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.Manager.Entities
{
    public class SendMessageRequestEntity
    {
        public string ApiKey { get; set; }
        public string Message { get; set; }
        public string Numbers { get; set; }
        public string SenderName { get; set; }
    }
}
