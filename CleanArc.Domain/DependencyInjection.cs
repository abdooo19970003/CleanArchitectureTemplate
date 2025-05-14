
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            // Register Domain services here
            return services;
        }
    }
}
