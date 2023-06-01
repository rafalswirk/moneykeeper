using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.DTO
{
    public record BudgetEntryDto(string TaxId, DateTime TransactionTime, double Total);
}
