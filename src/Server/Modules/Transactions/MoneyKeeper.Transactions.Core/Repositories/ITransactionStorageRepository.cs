using MoneyKeeper.Transactions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Repositories
{
    public interface ITransactionStorageRepository
    {
        Task AddAsync(Transaction transaction);
    }
}
