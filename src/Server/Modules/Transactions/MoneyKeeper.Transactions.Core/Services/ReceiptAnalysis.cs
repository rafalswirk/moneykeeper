﻿using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.DTO;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.OCR.GCloud;
using MoneyKeeper.Transactions.OCR.GCloud.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Services
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
                var jsonFilePath = Path.Combine(_dataDirectories.OcrResultsPath, Path.ChangeExtension(receiptInfo.ImageName, ".json"));
                if (_fileSystem.File.Exists(jsonFilePath))
                {
                    var bufferedReceipt = _parser.Parse(_fileSystem.File.ReadAllText(jsonFilePath));
                    return Map(bufferedReceipt);
                }
                var ocrRawData = await _imageProvider.SendImage(Path.Combine(_dataDirectories.ReceiptImagesPath, receiptInfo.ImageName));
                _fileSystem.File.WriteAllText(jsonFilePath, ocrRawData);
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
