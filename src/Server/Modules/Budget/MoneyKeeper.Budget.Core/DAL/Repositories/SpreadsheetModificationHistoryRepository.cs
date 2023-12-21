using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DAL.Repositories
{
    public class SpreadsheetModificationHistoryRepository : ISpreadsheetModificationHistory
    {
        private readonly BudgetCategoryDbContext _context;

        public SpreadsheetModificationHistoryRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }
        public async Task RecordModificationAsync(Entities.SpreadsheetModificationHistory modification)
        {
            await _context.ModificationHistory.AddAsync(modification);
            await _context.SaveChangesAsync();
        }
    }
}
