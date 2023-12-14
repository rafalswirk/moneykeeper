using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend
{
    internal interface ITransactionCategories
    {
        Task<IReadOnlyCollection<string>> GetCategories();
    }
}
