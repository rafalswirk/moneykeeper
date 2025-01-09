using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Exceptions;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Services.Storage;
using MoneyKeeper.UnitTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.UnitTests.Transactions.Tests.Storage
{
    public class ReceiptRemoveTestFixture
    {
        private readonly string _fileName;
        private readonly MockFileSystem _fileSystem = new();
        IReceiptInfoRepository _repository = new InMemoryReceiptRepository();
        private ReceiptInfo _receiptInfo;
        DataDirectories _dataDirectories = new DataDirectories(@"C:\mk-data\Receipts", @"C:\mk-data\OCR");
        ILogger<ReceiptRemove> _logger = A.Fake<Microsoft.Extensions.Logging.ILogger<ReceiptRemove>>();

        public ReceiptRemoveTestFixture()
        {
            _fileName = $"{Guid.NewGuid()}.jpg";
            _receiptInfo = new ReceiptInfo
            {
                ImageName = _fileName,
            };

        }

        [Fact]
        public async Task Remove_WithValidReceiptInfo_RemovingReceiptFromHardDriveAndDatabase()
        {
            _fileSystem.AddEmptyFile($"C:\\mk-data\\Receipts\\{_fileName}");
            await _repository.AddAsync(_receiptInfo);   
            var storageOperation = new ReceiptRemove(_repository, _fileSystem, Options.Create(_dataDirectories), _logger);

            await storageOperation.Remove(_receiptInfo);

            _fileSystem.Directory.GetFiles(@"C:\mk-data\Receipts").Length.ShouldBe(0);
            var receipts = await _repository.BrowseAsync();
            receipts.Where(r => r.ImageName == _fileName).Count().ShouldBe(0);
        }

        [Fact]
        public async Task Remove_WithNotExistingReceipt_ThrowsReceiptNotExistsException()
        {
            var storageOperation = new ReceiptRemove(_repository, _fileSystem, Options.Create(_dataDirectories), _logger);

            await storageOperation.Remove(_receiptInfo).ShouldThrowAsync<ReceiptNotFoundException>();
        }
    }
}
