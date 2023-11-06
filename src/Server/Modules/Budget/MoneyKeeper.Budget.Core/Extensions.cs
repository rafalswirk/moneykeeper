using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Repositories;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;

namespace MoneyKeeper.Budget
{
    public static class Extensions
    {
        public static IServiceCollection AddBudget(this IServiceCollection services)
        {
            services.AddDbContext<Budget.DAL.BudgetCategoryDbContext>(options =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetSection("Database:ConnectionString").Value);
            });

            services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
            services.AddScoped<ICategorySpreadsheetMapRepository, CategorySpreadsheetMapRepository>();
            services.AddScoped<ITaxMappingRepository, TaxIdMappingRepository>();
            services.AddScoped<ITaxIdRepository, TaxIdRepository>();
            services.AddScoped<ISheetToMonthMapRepository, SheetToMonthMapRepository>();

            services.AddScoped<ServiceLoader>(x =>
                new ServiceLoader(x.GetRequiredService<IConfiguration>().GetSection("GCloud:ServiceAccountFile").Value));
            services.AddScoped(x =>
                x.GetRequiredService<IConfiguration>()
                    .GetSection(nameof(DataDirectories))
                    .Get<DataDirectories>());
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
            return services;
        }
    }
}