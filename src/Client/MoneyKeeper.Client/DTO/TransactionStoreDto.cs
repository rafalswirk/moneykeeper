using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.DTO
{
    record TransactionStoreDto(double Value, DateTime Date, int ReceiptId);
}
