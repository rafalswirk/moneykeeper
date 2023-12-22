using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyKeeper.Transactions.Core.Entities;

namespace MoneyKeeper.Transactions.Core.Services
{
    public class TransactionsService
    {
        private readonly ITransactionStorageRepository _transactionStorage;
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public TransactionsService(ITransactionStorageRepository transactionStorage, IReceiptInfoRepository receiptInfoRepository)
        {
            _transactionStorage = transactionStorage;
            _receiptInfoRepository = receiptInfoRepository;
        }

        public async Task StoreTransaction(TransactionDto dto)
        {
            var receipt = await _receiptInfoRepository.GetAsync(dto.ReceiptId);
            var transaction = new Transaction 
            {
                Date = dto.Date,
                Value = dto.Value,
                Info = receipt,
                EntryDate = DateTime.Now
            };
            await _transactionStorage.AddAsync(transaction);
        }
    }
}
