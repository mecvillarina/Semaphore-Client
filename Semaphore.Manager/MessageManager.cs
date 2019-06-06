using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semaphore.Common.Enums;
using Semaphore.Common.Utilities;
using Semaphore.Manager.Entities;
using Semaphore.Manager.Mappers;
using Semaphore.WebService;
using Semaphore.WebService.DataContracts;

namespace Semaphore.Manager
{
    public class MessageManager : ManagerBase, IMessageManager
    {
        private readonly IMessageWebService _messageWebService;
        public MessageManager(IConnectivity connectivity, IServiceEntityMapper mapper, IMessageWebService messageWebService) : base(connectivity, mapper)
        {
            _messageWebService = messageWebService;
        }

        public async Task<SendMessagesResponseEntity> SendBulkMessage(SendMessageRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new SendMessagesResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<SendMessageRequestContract>(reqEntity);
            var respContract = await _messageWebService.SendBulkMessage(reqContract);
            var respEntity = Mapper.Map<SendMessagesResponseEntity>(respContract);

            return respEntity;
        }
    }
}
