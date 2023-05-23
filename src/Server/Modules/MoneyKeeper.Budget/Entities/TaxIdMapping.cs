using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Entities
{
    public class TaxIdMapping
    {
        public int Id { get; set; }
        public TaxId TaxId { get; set; }
        public BudgetCategory Category { get; set; }
    }
}
