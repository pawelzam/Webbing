using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webbing.Assignment.Service.Interfaces;
using Webbing.Assignment.Service.Services;

namespace Webbing.Assignment.Service;

public static class ServiceInstaller
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.EnableSensitiveDataLogging();
            options.UseInMemoryDatabase("InMemory");
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Repository).Assembly));
        services.AddScoped<IRepository, Repository>();
        services.AddHostedService<NetworkEventDispatcherService>();

        return services;
    }
}