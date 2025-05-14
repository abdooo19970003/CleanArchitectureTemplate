using CleanArc.Application.Configuration;
using CleanArc.Application.Validation;
using CleanArc.Domain;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            // Register application services here

            // MediatR Registration
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly)
            );

            // FluentValidation Registration
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipline<,>));

            // Mapster Registration
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
