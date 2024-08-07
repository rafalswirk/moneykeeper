﻿using MoneyKeeper.Transactions.Core.Repositories;
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
                TransactionDate = dto.Date.ToUniversalTime(),
                Value = dto.Value,
                ReceiptInfoId = dto.ReceiptId,
            };
            await _transactionStorage.AddAsync(transaction);
        }
    }
}
