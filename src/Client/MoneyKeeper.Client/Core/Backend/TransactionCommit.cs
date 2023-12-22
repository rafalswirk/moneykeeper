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
    //Todo Make communication between modules at server side, so from client perspective there is only post transaction
    internal class TransactionCommit
    {
        public async Task CommitTransactionAsync(TransactionData transaction)
        {
            await Consts.BaseApiUrl.AppendPathSegment("budget/transaction").PostJsonAsync(new TransactionDto(transaction.Date, transaction.CategoryId, transaction.Value));
            await Consts.BaseApiUrl.AppendPathSegment("transactions/spreadsheetEntered").PatchJsonAsync(new {id = transaction.ReceiptId });
            await Consts.BaseApiUrl.AppendPathSegment("transactions").PostJsonAsync(new TransactionStoreDto(transaction.Value, transaction.Date, 1));
        }
    }
}
