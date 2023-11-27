using MoneyKeeper.Transactions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Repositories
{
    public interface IReceiptInfoRepository
    {
        Task AddAsync(ReceiptInfo info);
        Task DeleteAsync(ReceiptInfo info);
        Task<IReadOnlyCollection<ReceiptInfo>> BrowseAsync();
        Task<ReceiptInfo> GetAsync(int id);
        Task UpdateAsync(ReceiptInfo info);
    }
}
