using FakeItEasy;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Exceptions;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Storage;
using MoneyKeeper.UnitTests.Mocks;
using Shouldly;
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
            var receiptInfo = new ReceiptInfo
            {
                ImageName = fileName,
            };
            repository.AddAsync(receiptInfo);
            var logger = A.Fake<Microsoft.Extensions.Logging.ILogger<ReceiptRemove>>();
            var storageOperation = new ReceiptRemove(repository, logger);

            await storageOperation.Remove(receiptInfo);

            fileSystem.Directory.GetFiles(@"C:\mk-data\Receipts").Length.ShouldBe(0);
            var receipts = await repository.BrowseAsync();
            receipts.Where(r => r.ImageName == fileName).Count().ShouldBe(0);
        }

        [Fact]
        public async Task Remove_WithNotExistingReceipt_ThrowsReceiptNotExistsException()
        {
            var fileSystem = new MockFileSystem();
            var repository = new InMemoryReceiptRepository();
            var logger = A.Fake<Microsoft.Extensions.Logging.ILogger<ReceiptRemove>>();
            var storageOperation = new ReceiptRemove(repository, logger);

            await storageOperation.Remove(new ReceiptInfo()).ShouldThrowAsync<ReceiptNotFoundException>();
        }
    }
}
