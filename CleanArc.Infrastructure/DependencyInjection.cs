using CleanArc.Application.Interfaces;
using CleanArc.Application.Repositories;
using CleanArc.Infrastructure.Context;
using CleanArc.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Register Infrastructure services here

            // Registering the UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // Registering the repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Registering the database context
            var connectionstring = configuration.GetConnectionString("Default");
            Console.WriteLine($"My Connection String:{connectionstring}");
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionstring)
            );

            return services;
        }
    }
}
