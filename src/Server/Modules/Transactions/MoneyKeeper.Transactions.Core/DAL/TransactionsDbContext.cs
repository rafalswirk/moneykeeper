using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Transactions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.DAL
{
    public class TransactionsDbContext : DbContext
    {
        public DbSet<ReceiptInfo> ReceiptInfos { get; set; }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
