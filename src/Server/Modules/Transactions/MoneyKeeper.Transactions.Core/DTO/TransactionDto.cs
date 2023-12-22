using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.DTO
{
    public record TransactionDto(double Value, DateTime Date, int ReceiptId);
}
