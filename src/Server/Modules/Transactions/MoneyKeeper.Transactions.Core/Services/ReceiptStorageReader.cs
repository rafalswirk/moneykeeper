using MoneyKeeper.Transactions.Core.DTO;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Services
{
    public class ReceiptStorageReader
    {
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public ReceiptStorageReader(IReceiptInfoRepository receiptInfoRepository)
        {
            _receiptInfoRepository = receiptInfoRepository;
        }

        public async Task<IReadOnlyList<ReceiptInfoDto>> GetReceipts()
        {
            var receipts = await _receiptInfoRepository.BrowseAsync();
            return receipts.Select(i => Map<ReceiptInfoDto>(i)).ToList();
        }

        private ReceiptInfoDto Map<T>(ReceiptInfo receipt)
            => new(receipt.Id, receipt.ImageName, receipt.OcrDataGenerated, receipt.OcrValidationResult,
                receipt.SpreadsheetEntered, new DateTime(receipt.UploadDate.Year, receipt.UploadDate.Month, receipt.UploadDate.Day));
    }
}
