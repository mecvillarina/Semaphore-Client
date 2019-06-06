using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.WebService.DataContracts
{
    public class SendMessageRequestContract
    {
        public string ApiKey { get; set; }
        public string Message { get; set; }
        public string Numbers { get; set; }
        public string SenderName { get; set; }
    }
}
