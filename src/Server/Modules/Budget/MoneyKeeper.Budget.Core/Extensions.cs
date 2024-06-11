using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Budget.Core.DAL.Repositories;
using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Entities;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Core.Services.Transactions;
using MoneyKeeper.Budget.DAL;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Repositories;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;

namespace MoneyKeeper.Budget
{
    public static class Extensions
    {
        private const string LocalEnvironment = "Local";

        public static IServiceCollection AddBudget(this IServiceCollection services, string environment)
        {
            if (environment.Equals(LocalEnvironment))
            {
                Console.WriteLine("Running with Sqlite");
                services.AddDbContext<BudgetCategoryDbContext>(options =>
                {
                    using var serviceProvider = services.BuildServiceProvider();
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    options.UseSqlite(configuration.GetSection("Local:Budget:Database:ConnectionString").Value);
                });
                services.ApplyMigrations<BudgetCategoryDbContext>();
            }
            else
            {
                Console.WriteLine("Running with Npqsql");
                services.AddDbContext<BudgetCategoryDbContext>(options =>
                {
                    using var serviceProvider = services.BuildServiceProvider();
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    options.UseNpgsql(configuration.GetConnectionString("BudgetDatabase"));
                });
                services.ApplyMigrations<BudgetCategoryDbContext>();
            }
            services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
            services.AddScoped<ICategorySpreadsheetMapRepository, CategorySpreadsheetMapRepository>();
            services.AddScoped<ITaxMappingRepository, TaxIdMappingRepository>();
            services.AddScoped<ITaxIdRepository, TaxIdRepository>();
            services.AddScoped<ISheetToMonthMapRepository, SheetToMonthInMemoryRepository>();

            services.AddScoped<ServiceLoader>(x =>
                new ServiceLoader(x.GetRequiredService<IConfiguration>().GetSection("GCloud:ServiceAccountFile").Value));
            services.AddSingleton(x =>
                x.GetRequiredService<IConfiguration>()
                    .GetSection(nameof(SpreadsheetSettings))
                    .Get<SpreadsheetSettings>());
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IDirectory, DirectoryWrapper>();
            services.AddScoped<SpreadsheetDataEditor>();
            services.AddScoped<GoogleDocsEditor>();
            services.AddScoped<BudgetCategoriesGenerator>();
            services.AddScoped<BudgetCategoryPositionGenerator>();
            services.AddScoped<CategoriesSetup>();
            services.AddScoped<ICategorySpreadsheetMapRepository, CategorySpreadsheetMapRepository>();
            services.AddScoped<IGoogleDocsEditor, GoogleDocsEditor>();
            services.AddScoped<CategoriesService>();
            services.AddScoped<ISpreadsheetModificationHistory, SpreadsheetModificationHistoryRepository>();
            services.AddScoped<ITransactionCreator, TransactionCreator>();
            services.AddScoped<ISpreadsheetRepository, SpreadsheetRepository>();

            return services;
        }
    }
}