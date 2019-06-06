using Semaphore.Manager.Entities;
using System.Threading.Tasks;

namespace Semaphore.Manager
{
	public interface IAccountManager
	{
		Task<SenderNamesResponseEntity> GetSenderNames(string apiKey);
	}
}