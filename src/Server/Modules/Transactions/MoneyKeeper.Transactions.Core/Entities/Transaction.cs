using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public ReceiptInfo Info { get; set; }
    }
}
