using AutoMapper;
using Semaphore.Manager.Entities;
using Semaphore.WebService.DataContracts;

namespace Semaphore.Manager.Mappers
{
	public class EntityContractProfile : Profile
    {
		public EntityContractProfile()
		{
			CreateMap<SenderNameEntity, SenderNameContract>();
			CreateMap<SenderNameContract, SenderNameEntity>();

			CreateMap<SenderNamesResponseEntity, SenderNamesResponseContract>();
			CreateMap<SenderNamesResponseContract, SenderNamesResponseEntity>();
		}
    }
}
