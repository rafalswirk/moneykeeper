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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class ReceiptAnalysis
    {
        private readonly DataDirectoriesWrapper _dataDirectories;
        private readonly IFileSystem _fileSystem;
        private readonly ImageProvider _imageProvider;
        private readonly IReceiptInfoRepository _receiptInfoRepository;
        private readonly BillOfSaleParser _parser = new BillOfSaleParser();

        public ReceiptAnalysis(DataDirectoriesWrapper dataDirectories, IFileSystem fileSystem, ImageProvider imageProvider, IReceiptInfoRepository receiptInfoRepository)
        {
            _dataDirectories = dataDirectories;
            _fileSystem = fileSystem;
            _imageProvider = imageProvider;
            _receiptInfoRepository = receiptInfoRepository;
        }   

        public async Task<ReceiptDto> MakeAnalysis(int receiptId)
        {
            try
            {
                var receiptInfo = await _receiptInfoRepository.GetAsync(receiptId);
                var ocrRawData = await _imageProvider.SendImage(Path.Combine(_dataDirectories.ReceiptImagesPath, receiptInfo.ImageName));
                _fileSystem.File.WriteAllText(Path.Combine(_dataDirectories.OcrResultsPath, $"{Path.GetFileNameWithoutExtension(receiptInfo.ImageName)}.json"), ocrRawData);
                receiptInfo.OcrDataGenerated = true;
                await _receiptInfoRepository.UpdateAsync(receiptInfo);
                var receipt = _parser.Parse(ocrRawData);
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
