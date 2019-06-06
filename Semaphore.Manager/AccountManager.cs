using Semaphore.Common.Enums;
using Semaphore.Common.Utilities;
using Semaphore.Manager.Entities;
using Semaphore.Manager.Mappers;
using Semaphore.WebService;
using System.Threading.Tasks;

namespace Semaphore.Manager
{
	public class AccountManager : ManagerBase, IAccountManager
	{
		private readonly IAccountWebService _accountWebService;
		public AccountManager(IConnectivity connectivity, IServiceEntityMapper mapper, IAccountWebService accountWebService) : base(connectivity, mapper)
		{
			_accountWebService = accountWebService;
		}

		public async Task<SenderNamesResponseEntity> GetSenderNames(string apiKey)
		{
			if (!Connectivity.IsInternetAvailable)
			{
				return new SenderNamesResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
			}

			var respContract = await _accountWebService.GetSenderNames(apiKey);
			var respEntity = Mapper.Map<SenderNamesResponseEntity>(respContract);

			return respEntity;
		}
	}
}
