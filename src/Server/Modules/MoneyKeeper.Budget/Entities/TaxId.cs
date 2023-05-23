using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Entities
{
    public class TaxId
    {
        public int Id { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string CompanyName { get; set; }
    }
}
