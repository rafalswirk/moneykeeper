using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Exceptions
{
    internal class ReceiptNotFoundException : TransactionBaseException
    {
        public ReceiptNotFoundException(string? message) : base(message)
        {
        }

        public ReceiptNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
