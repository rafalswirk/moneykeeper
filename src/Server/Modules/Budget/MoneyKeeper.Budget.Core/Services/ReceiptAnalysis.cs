using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Console.GCloud;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class ReceiptAnalysis
    {
        private readonly DataDirectories _dataDirectories;
        private readonly IFileSystem _fileSystem;
        private readonly ImageProvider _imageProvider;
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public ReceiptAnalysis(DataDirectories dataDirectories, IFileSystem fileSystem, ImageProvider imageProvider, IReceiptInfoRepository receiptInfoRepository)
        {
            _dataDirectories = dataDirectories;
            _fileSystem = fileSystem;
            _imageProvider = imageProvider;
            _receiptInfoRepository = receiptInfoRepository;
        }   

        public async Task<bool> MakeAnalysis(int receiptId)
        {
            try
            {
                var receiptInfo = await _receiptInfoRepository.GetAsync(receiptId);
                var ocrData = await _imageProvider.SendImage(Path.Combine(_dataDirectories.ReceiptImagesPath, receiptInfo.ImageName));
                _fileSystem.File.WriteAllText(Path.Combine(_dataDirectories.OcrResultsPath, $"{Path.GetFileNameWithoutExtension(receiptInfo.ImageName)}.json"), ocrData);
                receiptInfo.OcrDataGenerated = true;
                await _receiptInfoRepository.UpdateAsync(receiptInfo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
