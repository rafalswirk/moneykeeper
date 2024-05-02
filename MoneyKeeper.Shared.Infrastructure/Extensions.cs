using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Shared.Infrastructure.Exceptions;
using Serilog;

namespace MoneyKeeper.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services.AddSerilog(lc => lc
            .WriteTo.Console()
            .WriteTo.File("/logs/logs.txt", rollingInterval: RollingInterval.Day));
             
            services.AddErrorHandling();


            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandling();

            return app;
        }
    }
}