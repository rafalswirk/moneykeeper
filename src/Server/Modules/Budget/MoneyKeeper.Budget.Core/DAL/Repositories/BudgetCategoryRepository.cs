using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.DTO;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL.Repositories
{
    public class BudgetCategoryRepository : IBudgetCategoryRepository
    {
        private readonly DbSet<BudgetCategory> _budgetCategory;
        private readonly BudgetCategoryDbContext _context;

        public BudgetCategoryRepository(BudgetCategoryDbContext context)
        {
            _budgetCategory = context.BudgetCategories;
            _context = context;
        }

        public async Task AddAsync(BudgetCategory category)
        {
            await _budgetCategory.AddAsync(category);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(BudgetCategory category)
        {
            _budgetCategory.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<BudgetCategory>> BrowseAsync()
            => await _budgetCategory.ToListAsync();

        public async Task<BudgetCategory> GetAsync(int id)
            => await _budgetCategory.SingleOrDefaultAsync(x => x.Id == id);
        

        public async Task UpdateCategoryAsync(BudgetCategory category)
        {
            _budgetCategory.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
