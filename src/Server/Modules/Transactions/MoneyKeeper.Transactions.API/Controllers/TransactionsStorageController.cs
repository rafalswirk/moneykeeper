using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyKeeper.Transactions.Core.DTO;
using MoneyKeeper.Transactions.Core.Services;

namespace MoneyKeeper.Transactions.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsStorageController : ControllerBase
    {
        private readonly TransactionsService _transactionsService;

        public TransactionsStorageController(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionAsync(TransactionDto transaction)
        {
            await _transactionsService.StoreTransaction(transaction);
            return Ok();
        }
    }
}
