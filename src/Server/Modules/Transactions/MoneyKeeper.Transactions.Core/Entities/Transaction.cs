using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Entities
{
    internal class Transaction
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public ReceiptInfo Info { get; set; }
    }
}
