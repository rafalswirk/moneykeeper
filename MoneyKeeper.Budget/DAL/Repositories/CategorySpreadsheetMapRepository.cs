using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL.Repositories
{
    internal class CategorySpreadsheetMapRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public CategorySpreadsheetMapRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CategorySpreadsheetMap category)
        {
            _context.CategoryMap.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategorySpreadsheetMap category)
        {
            _context.CategoryMap.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<CategorySpreadsheetMap>> BrowseAsync()
            => await _context.CategoryMap.ToListAsync();

        public async Task<CategorySpreadsheetMap> GetAsync(int id)
            => await _context.CategoryMap.SingleOrDefaultAsync(x => x.Id == id);

        public async Task UpdateCategoryAsync(CategorySpreadsheetMap category)
        {
            _context.CategoryMap.Update(category);
            await _context.SaveChangesAsync();
        }

    }
}
