using Semaphore.WebService.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.WebServices;

namespace Semaphore.WebService
{
    public interface IMessageWebService
    {
        Task<SendMessagesResponseContract> SendBulkMessage(SendMessageRequestContract reqContract);
    }

    public class MessageWebService : WebServiceBase, IMessageWebService
    {
        public async Task<SendMessagesResponseContract> SendBulkMessage(SendMessageRequestContract reqContract)
        {
            string endpoint = $"api/v4/messages?apikey={reqContract.ApiKey}&number={reqContract.Numbers}&message={reqContract.Message}&sendername={reqContract.SenderName}";
            var responseTuple = await PostAsync<List<SendMessageResponseContract>, SendMessageRequestContract>(endpoint, new SendMessageRequestContract());

            var resultContract = new SendMessagesResponseContract();
            resultContract.SendMessages = responseTuple.Item2;
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }
    }

}
