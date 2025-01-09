using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.UnitTests.Mocks
{
    internal class InMemoryReceiptRepository : IReceiptInfoRepository
    {
        private List<ReceiptInfo> _receiptInfos = [];

        public Task AddAsync(ReceiptInfo info)
        {
            _receiptInfos.Add(info);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<ReceiptInfo>> BrowseAsync()
        {
            var r = _receiptInfos.AsReadOnly<ReceiptInfo>();
            return Task.FromResult((IReadOnlyCollection<ReceiptInfo>)_receiptInfos.AsReadOnly<ReceiptInfo>());
        }

        public Task DeleteAsync(ReceiptInfo info)
        {
            _receiptInfos.Remove(info);
            return Task.CompletedTask;
        }

        public Task<ReceiptInfo> GetAsync(int id)
            => Task.FromResult(_receiptInfos.SingleOrDefault(r => r.Id == id));

        public Task UpdateAsync(ReceiptInfo info)
        {
            var receipt = _receiptInfos.Single(r => r.Id == info.Id);
            receipt.ImageName = info.ImageName;
            receipt.SpreadsheetEntered = info.SpreadsheetEntered;
            receipt.SpreadsheetEnterTime = info.SpreadsheetEnterTime;
            receipt.UploadDate = info.UploadDate;
            receipt.OcrValidationResult = info.OcrValidationResult;
            receipt.OcrDataGenerated = info.OcrDataGenerated;
            return Task.CompletedTask;
        }
    }
}
