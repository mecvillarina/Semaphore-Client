using Semaphore.Manager.Entities;
using System.Threading.Tasks;

namespace Semaphore.Manager
{
    public interface IMessageManager
    {
        Task<SendMessagesResponseEntity> SendBulkMessage(SendMessageRequestEntity reqEntity);
    }
}