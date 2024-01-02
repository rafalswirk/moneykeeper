using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? ReceiptInfoId { get; set; }
        public ReceiptInfo? ReceiptInfo { get; set; }
    }
}
