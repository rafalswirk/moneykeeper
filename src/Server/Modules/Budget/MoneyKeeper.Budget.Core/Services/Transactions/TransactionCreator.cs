using MoneyKeeper.Budget.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.Transactions
{
    public class TransactionCreator
    {
        private ISpreadsheetRepository _spreadsheetRepository;

        public TransactionCreator(ISpreadsheetRepository spreadsheetRepository)
        {
            _spreadsheetRepository = spreadsheetRepository;
        }

        internal void Create(DTO.TransactionDto transactionDto)
        {
            throw new NotImplementedException();
        }
    }
}
