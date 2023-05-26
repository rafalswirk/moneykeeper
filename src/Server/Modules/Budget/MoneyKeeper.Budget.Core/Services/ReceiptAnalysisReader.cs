using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Console.GCloud;
using MoneyKeeper.OCR.GCloud;
using MoneyKeeper.OCR.GCloud.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class ReceiptAnalysisReader
    {
        private readonly DataDirectoriesWrapper _dataDirectories;
        private readonly IFileSystem _fileSystem;
        private readonly IReceiptInfoRepository _receiptInfoRepository;
        private readonly BillOfSaleParser _parser = new BillOfSaleParser();

        public ReceiptAnalysisReader(DataDirectoriesWrapper dataDirectories, IFileSystem fileSystem, IReceiptInfoRepository receiptInfoRepository)
        {
            _dataDirectories = dataDirectories;
            _fileSystem = fileSystem;
            _receiptInfoRepository = receiptInfoRepository;
        }

        public async Task<ReceiptDto> ReadAnalysis(int receiptId)
        {
            try
            {
                var receiptInfo = await _receiptInfoRepository.GetAsync(receiptId);
                if (!receiptInfo.OcrDataGenerated)
                    return null;
                var ocrFilePath = Path.Combine(_dataDirectories.OcrResultsPath, $"{Path.GetFileNameWithoutExtension(receiptInfo.ImageName)}.json");
                var receipt = _parser.Parse(_fileSystem.File.ReadAllText(ocrFilePath));
                return Map(receipt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private ReceiptDto Map(Receipt receipt)
            => new ReceiptDto
            {
                Date = new DateTime(receipt.Date.Year, receipt.Date.Month, receipt.Date.Day),
                TaxNumber = receipt.TaxNumber,
                Total = receipt.Total
            };
    }
}
