using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Core.Entities;
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
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<CategorySpreadsheetMap> CategoryMap { get; set; }
        public DbSet<TaxIdMapping> TaxIdMapping { get; set; }
        public DbSet<TaxId> TaxIds { get; set; }
        public DbSet<SheetToMonthMap> SheetToMonthMap { get; set; }
        public DbSet<SpreadsheetModificationHistory> ModificationHistory { get; set; }
        public BudgetCategoryDbContext(DbContextOptions<BudgetCategoryDbContext> options) : base(options)
        {
        }
    }
}
