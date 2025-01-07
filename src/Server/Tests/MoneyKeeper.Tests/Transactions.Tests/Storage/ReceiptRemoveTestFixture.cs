﻿using FakeItEasy;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Storage;
using MoneyKeeper.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.UnitTests.Transactions.Tests.Storage
{
    public class ReceiptRemoveTestFixture
    {
        [Fact]
        public async Task Remove_WithValidReceiptInfo_RemovingReceiptFromHardDriveAndDatabase()
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var fileSystem = new MockFileSystem();
            fileSystem.AddEmptyFile($"C:\\mk-data\\Receipts\\{fileName}");
            var repository = new InMemoryReceiptRepository();
            repository.AddAsync(new ReceiptInfo 
            {
                ImageName = fileName,
            });
            var logger = A.Fake<Microsoft.Extensions.Logging.ILogger<ReceiptRemove>>();
            var storageOperation = new ReceiptRemove(repository, logger);

            await storageOperation.Remove(new ReceiptInfo());
            
            Assert.Equal(0, fileSystem.Directory.GetFiles(@"C:\mk-data\Receipts").Length);
            var receipts = await repository.BrowseAsync();
            Assert.Equal(0, receipts.Where(r => r.ImageName == fileName).Count());
        }

        [Fact]
        public void Remove_WithNotExistingReceipt_ThrowsReceiptNotExistsException()
        {
            throw new NotImplementedException();
        }
    }
}
