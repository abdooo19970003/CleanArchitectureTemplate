using Mapster;
using MapsterMapper;

namespace CleanArc.Application.Configuration
{
    public class ServiceMapper : IMapper
    {
        private readonly TypeAdapterConfig _config;
        public ServiceMapper(TypeAdapterConfig config)
        {
            _config = config;
        }

        public TypeAdapterConfig Config => throw new NotImplementedException();

        public ITypeAdapterBuilder<TSource> From<TSource>(TSource source)
        {
            return From(source);
        }
        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>(_config);
        }
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TSource, TDestination>(_config);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }

}
