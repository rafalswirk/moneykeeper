using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.DTO
{
    public class ReceiptDto
    {
        public string TaxNumber { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
    }
}
