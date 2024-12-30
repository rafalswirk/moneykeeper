using Microsoft.Extensions.Logging;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Entities;
using MoneyKeeper.Transactions.Core.Exceptions;
using MoneyKeeper.Transactions.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Storage
{
    public class ReceiptRemove
    {
        private readonly IReceiptInfoRepository _receiptInfoRepository;
        private readonly ILogger _logger;

        public ReceiptRemove(IReceiptInfoRepository receiptInfoRepository, ILogger logger)
        {
            _receiptInfoRepository = receiptInfoRepository;
            _logger = logger;
        }
        public async Task Remove(ReceiptInfo receipt)
        {
            try
            {
                await _receiptInfoRepository.DeleteAsync(receipt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot remove receipt from repository");
                throw new ReceiptCannotBeRemoved("Cannot remove receipt from repository", ex);
            }

            throw new NotImplementedException();
            //todo remove image with receipt
        }
    }
}
