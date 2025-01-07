using FakeItEasy;
using Microsoft.Extensions.Logging;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Repositories;
using MoneyKeeper.Transactions.Core.Storage;
using MoneyKeeper.UnitTests.Mocks;
using System;
using System.Collections.Generic;
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
            var repository = new InMemoryReceiptRepository();
            var logger = A.Fake(ILogger);
            var storageOperation = new ReceiptRemove(repository, logger);
            await storageOperation.Remove(new ReceiptInfo());

            throw new NotImplementedException();
        }

        [Fact]
        public void Remove_WithNotExistingReceipt_ThrowsReceiptNotExistsException()
        {
            throw new NotImplementedException();
        }
    }
}
