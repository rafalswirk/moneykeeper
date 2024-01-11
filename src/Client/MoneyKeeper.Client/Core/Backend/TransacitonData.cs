using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend
{
    internal record TransactionData(double Value, DateTime Date, int CategoryId, int ReceiptId);
}
