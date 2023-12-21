using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoneyKeeper.Transactions.Core.Repositories
{
    public interface ITransactionStorageRepository
    {
        Task AddAsync(Transaction transaction);
    }
}
