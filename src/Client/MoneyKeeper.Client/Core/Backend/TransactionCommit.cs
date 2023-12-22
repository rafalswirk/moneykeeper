using Flurl;
using Flurl.Http;
using MoneyKeeper.Client.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend
{
    internal class TransactionCommit
    {
        public async Task CommitTransactionAsync(TransactionData transaction)
        {
            await Task.WhenAll(
                Consts.BaseApiUrl.AppendPathSegment("budget/transaction").PostJsonAsync(new TransactionDto(transaction.Date, transaction.CategoryId, transaction.Value)),
                Consts.BaseApiUrl.AppendPathSegment("transactions").PostJsonAsync(new TransactionStoreDto(transaction.Value, transaction.Date, 1))
            );
        }
    }
}
