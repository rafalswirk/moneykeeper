using MoneyKeeper.Budget.Core.Entities;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Repositories
{
    internal interface IReceiptInfoRepository
    {
        Task AddAsync(ReceiptInfo info);
        Task DeleteAsync(ReceiptInfo info);
        Task<IReadOnlyCollection<ReceiptInfo>> BrowseAsync();
        Task<ReceiptInfo> GetAsync(int id);
        Task UpdateAsync(ReceiptInfo info);
    }
}
