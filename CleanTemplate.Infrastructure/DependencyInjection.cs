using System;
using CleanTemplate.Application.Interfaces.Persistence;
using CleanTemplate.Application.Interfaces.Services;
using CleanTemplate.Infrastructure.Persistence.Context;
using CleanTemplate.Infrastructure.Persistence.Repositories;
using CleanTemplate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure services registration goes here
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("TemplateConnection"),
                b => b.MigrationsAssembly(assembly)
            )
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)) ;
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();
        return services;
    }   
}
