using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoneyKeeper.Transactions.Core.DAL.Repositories
{
    public class TransactionsStorageRepository : ITransactionStorageRepository
    {
        private readonly TransactionsDbContext _context;

        public TransactionsStorageRepository(TransactionsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
