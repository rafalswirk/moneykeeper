using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL
{
    public class BudgetCategoryDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public BudgetCategoryDbContext(DbContextOptions<BudgetCategoryDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
