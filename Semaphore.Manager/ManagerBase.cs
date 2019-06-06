using Semaphore.Common.Utilities;
using Semaphore.Manager.Mappers;

namespace Semaphore.Manager
{
	public class ManagerBase
	{
		protected readonly IConnectivity Connectivity;
		protected readonly IServiceEntityMapper Mapper;
		public ManagerBase(IConnectivity connectivity, IServiceEntityMapper mapper)
		{
			Connectivity = connectivity;
			Mapper = mapper;
		}
	}
}
