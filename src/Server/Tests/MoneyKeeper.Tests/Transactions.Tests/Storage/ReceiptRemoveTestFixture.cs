using FakeItEasy;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Storage;
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
        public void Remove_WithValidReceiptInfo_RemovingReceiptFromHardDriveAndDatabase()
        {
            var storageOperation = new ReceiptRemove(null, null);
            storageOperation.Remove(new ReceiptInfo());

            throw new NotImplementedException();
        }

        [Fact]
        public void Remove_WithNotExistingReceipt_ThrowsReceiptNotExistsException()
        {
            throw new NotImplementedException();
        }
    }
}
