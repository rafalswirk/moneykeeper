using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Budget.Core.DAL.Repositories;
using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Repositories;
using MoneyKeeper.Console.GCloud;
using SixLabors.ImageSharp;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;

namespace MoneyKeeper.Budget
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
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
            services.AddScoped<IReceiptInfoRepository, ReceiptInfoRepository>();

            services.AddScoped<RecepitStorage>();
            services.AddScoped(x =>
                new ImageProvider(
                    x.GetRequiredService<IConfiguration>().GetSection("GCloud:AccessToken").Value,
                    x.GetRequiredService<IConfiguration>().GetSection("GCloud:ProjectId").Value));
            services.AddScoped<ReceiptAnalysis>();
            services.AddScoped(x =>
                x.GetRequiredService<IConfiguration>()
                    .GetSection(nameof(DataDirectories))
                    .Get<DataDirectories>());
            services.AddScoped<DataDirectoriesWrapper>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IDirectory, DirectoryWrapper>();

            return services;
        }
    }
}