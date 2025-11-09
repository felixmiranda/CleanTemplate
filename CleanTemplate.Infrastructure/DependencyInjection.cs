using System;
using CleanTemplate.Infrastructure.Persistence.Context;
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

        return services;
    }   
}
