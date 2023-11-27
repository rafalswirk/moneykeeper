using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.OCR.GCloud.Models
{
    public record Receipt(string TaxNumber, DateOnly Date, double Total);
}
