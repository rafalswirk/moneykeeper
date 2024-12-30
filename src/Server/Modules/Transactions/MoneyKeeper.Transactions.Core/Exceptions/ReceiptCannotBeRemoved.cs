using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Exceptions
{
    internal class ReceiptCannotBeRemoved : TransactionBaseException
    {
        public ReceiptCannotBeRemoved(string? message) : base(message)
        {
        }

        public ReceiptCannotBeRemoved(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
