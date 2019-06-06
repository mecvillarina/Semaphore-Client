using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Semaphore.WebService.DataContracts
{
    public class SendMessagesResponseContract : ContractBase
    {
        public List<SendMessageResponseContract> SendMessages { get; set; }
    }
    public class SendMessageResponseContract
    {
        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
