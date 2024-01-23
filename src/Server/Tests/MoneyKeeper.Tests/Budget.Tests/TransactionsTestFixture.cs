using MoneyKeeper.Budget.Core.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyKeeper.Budget.Core.DTO;

namespace MoneyKeeper.UnitTests.Budget.Tests
{
    public class TransactionsTestFixture
    {
        public void Create_TransactionFor2024_CreatingTransactionInRepositoryFor2024Year()
        {
            var creator = new TransactionCreator();
            creator.Create(new TransactionDto(new DateTime(2024, 4, 22), 1, 22.4));
        }
    }
}
