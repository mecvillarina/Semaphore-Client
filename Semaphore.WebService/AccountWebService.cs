using Semaphore.WebService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.WebServices;

namespace Semaphore.WebService
{
	public class AccountWebService : WebServiceBase, IAccountWebService
	{
		public async Task<SenderNamesResponseContract> GetSenderNames(string apiKey)
		{
			string endpoint = $"api/v4/account/sendernames?apikey={apiKey}";
			var responseTuple = await GetAsync<List<SenderNameContract>>(endpoint);

			var resultContract = new SenderNamesResponseContract();
			resultContract.SenderNames = responseTuple.Item2;
			resultContract.StatusCode = responseTuple.Item4;
			return resultContract;
		}
	}
}
