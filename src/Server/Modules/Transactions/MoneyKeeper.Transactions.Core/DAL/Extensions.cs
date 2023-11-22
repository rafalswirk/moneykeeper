using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.DAL
{
    internal static class Extensions
    {
        public static void ApplyMigrations(this IServiceCollection services)
        {
            using var transactionsContext = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<TransactionsDbContext>();
            transactionsContext.Database.Migrate();
        }
    }
}
