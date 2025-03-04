﻿using MoneyKeeper.Transactions.Core.DTO;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Services.Storage
{
    public class ReceiptUpdate
    {
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public ReceiptUpdate(IReceiptInfoRepository receiptInfoRepository)
        {
            _receiptInfoRepository = receiptInfoRepository;
        }

        public async Task SetReceiptEnteredToSpreadsheet(int receiptInfoId)
        {
            var info = await _receiptInfoRepository.GetAsync(receiptInfoId);
            info.SpreadsheetEnterTime = DateTime.UtcNow;
            info.SpreadsheetEntered = true;
            await _receiptInfoRepository.UpdateAsync(info);
        }
    }
}
