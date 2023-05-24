using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL.Repositories
{
    public class SheetToMonthMapRepository : ISheetToMonthMapRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public SheetToMonthMapRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SheetToMonthMap map)
        {
            await _context.SheetToMonthMap.AddAsync(map);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<SheetToMonthMap>> BrowseAsync()
            => await _context.SheetToMonthMap.ToListAsync();

        public async Task DeleteAsync(SheetToMonthMap map)
        {
            _context.SheetToMonthMap.Remove(map);
            await _context.SaveChangesAsync();
        }

        public async Task<SheetToMonthMap> GetAsync(int id)
            => await _context.SheetToMonthMap.SingleAsync(s => s.Id == id);

        public async Task UpdateAsync(SheetToMonthMap map)
        {
            _context.SheetToMonthMap.Update(map);
            await _context.SaveChangesAsync();
        }
    }
}
