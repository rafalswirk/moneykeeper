using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Transactions.Core.DAL;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.DAL.Repositories
{
    public class ReceiptInfoRepository : IReceiptInfoRepository
    {
        private readonly TransactionsDbContext _context;

        public ReceiptInfoRepository(TransactionsDbContext context)
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
            => await _context.ReceiptInfos.SingleOrDefaultAsync(r => r.Id == id);

        public async Task UpdateAsync(ReceiptInfo info)
        {
            _context.ReceiptInfos.Update(info);
            await _context.SaveChangesAsync();
        }
    }
}
