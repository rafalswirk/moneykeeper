using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Exceptions
{
    internal class TransactionBaseException : Exception
    {
        public TransactionBaseException(string? message) : base(message)
        {
        }

        public TransactionBaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
