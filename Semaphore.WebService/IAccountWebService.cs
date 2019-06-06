using Semaphore.WebService.DataContracts;
using System.Threading.Tasks;

namespace Semaphore.WebService
{
	public interface IAccountWebService
	{
		Task<SenderNamesResponseContract> GetSenderNames(string apiKey);
	}
}