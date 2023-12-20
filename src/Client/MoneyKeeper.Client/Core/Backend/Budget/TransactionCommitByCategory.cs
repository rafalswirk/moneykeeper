using Flurl;
using Flurl.Http;
using MoneyKeeper.Client.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend.Budget
{
    internal class TransactionCommitByCategory
    {
        public async Task CommitTransactionAsync(TransactionDto transaction)
        {
            await Consts.BaseApiUrl.AppendPathSegment("budget/transaction").PostJsonAsync(transaction);
        }
    }
}
