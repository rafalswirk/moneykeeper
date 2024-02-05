using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Core.Entities;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DAL.Repositories
{
    internal class SpreadsheetRepository : ISpreadsheetRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public SpreadsheetRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Spreadsheet>> BrowseAsync()
            => await _context.Spreadsheets.ToListAsync();

        public async Task<Spreadsheet> GetSpreadsheetByYear(int year)
            => await _context.Spreadsheets.SingleAsync(s => s.Year == year);
    }
}
