using System;

namespace Semaphore.Manager.Mappers
{
	public class ServiceEntityMapper : IServiceEntityMapper
    {
        public ServiceEntityMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<ModelProfile>();
                cfg.AddProfile<EntityContractProfile>();
                //cfg.AddProfile<EntityModelProfile>();
            });

            try
            {
                AutoMapper.Mapper.AssertConfigurationIsValid();
            }
            catch(Exception ex)
            {

            }
        }

        public TDestination Map<TSource, TDestination>(TSource value)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(value);
        }

        public TDestination Map<TDestination>(object value)
        {
            return AutoMapper.Mapper.Map<TDestination>(value);
        }
    }
}
