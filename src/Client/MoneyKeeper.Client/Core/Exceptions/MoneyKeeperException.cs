using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Exceptions
{
    internal class MoneyKeeperException: Exception
    {
        public MoneyKeeperException(string message)
            :base(message)
        {
                
        }

        public MoneyKeeperException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
