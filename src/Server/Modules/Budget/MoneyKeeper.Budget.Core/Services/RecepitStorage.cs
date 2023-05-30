﻿using Microsoft.AspNetCore.Http;
using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Entities;
using MoneyKeeper.Budget.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class RecepitStorage
    {
        private readonly string _imageDirectoryPath;
        private readonly IReceiptInfoRepository _receiptInfoRepository;

        public RecepitStorage(DataDirectoriesWrapper dataDirectories, IReceiptInfoRepository receiptInfoRepository)
        {
            _receiptInfoRepository = receiptInfoRepository;

            _imageDirectoryPath = dataDirectories.ReceiptImagesPath;
        }

        public async Task<ReceiptInfo> SaveReceipt(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_imageDirectoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var uploadTime = DateTime.Now;
                var receiptInfo = new ReceiptInfo
                {
                    ImageName = fileName,
                    OcrDataGenerated = false,
                    OcrValidationResult = false,
                    SpreadsheetEntered = false,
                    UploadDate = new DateOnly(uploadTime.Year, uploadTime.Month, uploadTime.Day)
                };
                await _receiptInfoRepository.AddAsync(receiptInfo);

                return receiptInfo;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}