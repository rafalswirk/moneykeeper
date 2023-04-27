using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL
{
    internal class BudgetCategoryDbContext : DbContext
    {
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public BudgetCategoryDbContext(DbContextOptions<BudgetCategoryDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
