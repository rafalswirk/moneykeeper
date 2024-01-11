using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Exceptions
{
    internal class TransactionCommitError : MoneyKeeperException
    {
        public TransactionCommitError(string message) : base(message)
        {
        }

        public TransactionCommitError(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
