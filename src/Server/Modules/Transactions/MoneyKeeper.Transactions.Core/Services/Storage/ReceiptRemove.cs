﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Exceptions;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Services.Storage
{
    public class ReceiptRemove
    {
        private readonly IReceiptInfoRepository _receiptInfoRepository;
        private readonly IFileSystem _fileSystem;
        private readonly ILogger<ReceiptRemove> _logger;
        private readonly string _imagesPath;

        public ReceiptRemove(IReceiptInfoRepository receiptInfoRepository, IFileSystem fileSystem, DataDirectoriesWrapper dataDirectories, ILogger<ReceiptRemove> logger)
        {
            _receiptInfoRepository = receiptInfoRepository;
            _fileSystem = fileSystem;
            _logger = logger;
            _imagesPath = dataDirectories.ReceiptImagesPath;
        }
        public async Task Remove(int id)
        {
            try
            {
                var receipt = await _receiptInfoRepository.GetAsync(id);
                if(receipt is null)
                    throw new ReceiptNotFoundException($"Receipt {id} cannot be found!");
                var receiptFullPath = Path.Combine(_imagesPath, receipt.ImageName);
                if (!_fileSystem.File.Exists(receiptFullPath))
                    throw new ReceiptNotFoundException($"Receipt {receipt.ImageName} cannot be found!");
                await _receiptInfoRepository.DeleteAsync(receipt);
                _fileSystem.File.Delete(receiptFullPath);
            }
            catch (ReceiptNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot remove receipt from repository");
                throw new ReceiptCannotBeRemoved("Cannot remove receipt from repository", ex);
            }
        }
    }
}
