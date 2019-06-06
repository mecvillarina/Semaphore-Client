using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore.Manager.Entities
{
    public class SendMessagesResponseEntity : EntityBase
    {
        public List<SendMessageResponseEntity> SendMessages { get; set; }
    }
    public class SendMessageResponseEntity
    {
        public long MessageId { get; set; }
        public long UserId { get; set; }
        public string User { get; set; }
        public long AccountId { get; set; }
        public string Account { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
