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
    public class ReceiptInfoRepository : IReceiptInfoRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public ReceiptInfoRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ReceiptInfo info)
        {
            await _context.ReceiptInfos.AddAsync(info);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<ReceiptInfo>> BrowseAsync()
            => await _context.ReceiptInfos.ToListAsync();

        public async Task DeleteAsync(ReceiptInfo info)
        {
            _context.ReceiptInfos.Remove(info);
            await _context.SaveChangesAsync();
        }

        public async Task<ReceiptInfo> GetAsync(int id)
            => await _context.ReceiptInfos.SingleAsync(r => r.Id == id);

        public async Task UpdateAsync(ReceiptInfo info)
        {
            _context.ReceiptInfos.Update(info);
            await _context.SaveChangesAsync();
        }
    }
}
