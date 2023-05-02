using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Console.GCloud;
using MoneyKeeper.OCR.GCloud;

namespace MoneyKeeper.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<Budget.DAL.BudgetCategoryDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                }
            }

            await host.RunAsync();

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<GCloudDemo>();
                    services.AddDbContext<Budget.DAL.BudgetCategoryDbContext>(options =>
                    {
                        using var serviceProvider = services.BuildServiceProvider();
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        options.UseNpgsql(configuration.GetSection("Database:ConnectionString").Value);
                    });
                    services.AddHostedService<GCloudDemo>();
                })
                .ConfigureAppConfiguration(x =>
                {
                    x.AddUserSecrets<Program>();
                })
                .UseConsoleLifetime();
    }
}