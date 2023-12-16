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
    internal class TransactionCategories : ITransactionCategories
    {
        public async Task<IReadOnlyCollection<BudgetCategoryDto>> GetCategories()
        {
            var categories = await Consts.BaseApiUrl.AppendPathSegment("budget/categories").GetAsync().ReceiveJson<List<BudgetCategoryDto>>();
            return categories;
        }
    }
}
