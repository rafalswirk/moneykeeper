using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Console.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class ReceiptAnalysis
    {
        private readonly ImageProvider _imageProvider;
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public ReceiptAnalysis(ImageProvider imageProvider, IReceiptInfoRepository receiptInfoRepository)
        {
            _imageProvider = imageProvider;
            _receiptInfoRepository = receiptInfoRepository;
        }

        public async Task<string> Analysis(int receiptId)
        {
            var receiptInfo = await _receiptInfoRepository.GetAsync(receiptId);
            var ocrData = await _imageProvider.SendImage(receiptInfo.ImageName);
            throw new NotImplementedException();
        }
    }
}
