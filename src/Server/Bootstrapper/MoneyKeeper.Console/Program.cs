using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Budget.Core.DAL.Repositories;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.DAL;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using MoneyKeeper.Console.GCloud;
using MoneyKeeper.OCR.GCloud;
using MoneyKeeper.Budget;

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
                    services.AddCore();
                })
                .ConfigureAppConfiguration(x =>
                {
                    x.AddUserSecrets<Program>();
                })
                .UseConsoleLifetime();
    }
}