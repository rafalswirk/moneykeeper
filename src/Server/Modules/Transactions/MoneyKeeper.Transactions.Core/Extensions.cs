﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Transactions.Core.DAL.Repositories;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Services;
using MoneyKeeper.Transactions.Core.Storage;
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
        public static IServiceCollection AddTransactions(this IServiceCollection services)
        {
            services.AddScoped<IReceiptInfoRepository, ReceiptInfoRepository>();
            services.AddScoped<RecepitStorage>();
            services.AddScoped<DataDirectoriesWrapper>();
            services.AddScoped(x =>
                new ImageProvider(x.GetRequiredService<IConfiguration>().GetSection("GCloud:ApiKey").Value));
            services.AddScoped<ReceiptAnalysis>();
            services.AddScoped<ReceiptAnalysisReader>();

            return services;
        }
    }
}
