﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MoneyKeeper.Transactions.Core.DAL;
using MoneyKeeper.Transactions.Core.DAL.Repositories;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Services;
using MoneyKeeper.Transactions.Core.Services.Storage;
using MoneyKeeper.Transactions.OCR.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core
{
    public static class Extensions
    {
        private const string LocalEnvironment = "Local";
        public static IServiceCollection AddTransactions(this IServiceCollection services, string environment)
        {
            services.AddDbContext<TransactionsDbContext>(options =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                if (environment.Equals(LocalEnvironment))
                {
                    Console.WriteLine("Running with Sqlite");
                    options.UseSqlite(configuration.GetConnectionString("TransactionsDatabase"));
                }
                else
                {
                    Console.WriteLine("Running with Npqsql");
                    options.UseNpgsql(configuration.GetConnectionString("TransactionsDatabase"));
                }
            });
            services.ApplyMigrations<TransactionsDbContext>();
            
            services.AddScoped<IReceiptInfoRepository, ReceiptInfoRepository>();
            services.AddScoped<ITransactionStorageRepository, TransactionsStorageRepository>();
            services.AddScoped<RecepitStorage>();
            services.AddScoped<DataDirectoriesWrapper>();
            services.AddScoped(x =>
                x.GetRequiredService<IConfiguration>()
                    .GetSection(nameof(DataDirectories))
                    .Get<DataDirectories>());
            services.AddScoped(x =>
                new ImageProvider(x.GetRequiredService<IConfiguration>().GetSection("GCloud:ApiKey").Value));
            services.AddScoped<ReceiptAnalysis>();
            services.AddScoped<ReceiptAnalysisReader>();
            services.AddScoped<ReceiptStorageReader>();
            services.AddScoped<TransactionsService>();
            services.AddScoped<ReceiptUpdate>();
            services.AddScoped<ReceiptRemove>();

            return services;
        }
    }
}
