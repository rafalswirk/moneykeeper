using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Budget;
using MoneyKeeper.Budget.Core;
using MoneyKeeper.Budget.Core.DAL;
using MoneyKeeper.Budget.DAL;
using MoneyKeeper.Transactions.Core;

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
                    var dbContext = services.GetRequiredService<BudgetCategoryDbContext>();
                    
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                }
                var gloud = services.GetService<GCloudDemo>();
                await gloud.Run();
            }

            await host.RunAsync();

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddBudget();
                    services.AddTransactions(hostContext.HostingEnvironment.EnvironmentName);
                    services.AddScoped<GCloudDemo>();
                })
                .ConfigureAppConfiguration(x =>
                {
                    x.AddUserSecrets<Program>();
                })
                .UseConsoleLifetime();
    }
}