using Mapster;

namespace CleanArc.Application.Configuration
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Add your mappings here
            // config.NewConfig<SourceType, DestinationType>();
        }
    }

}
