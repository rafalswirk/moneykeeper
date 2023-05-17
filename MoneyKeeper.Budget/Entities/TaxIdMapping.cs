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
        public string TaxId { get; set; }
        public CategorySpreadsheetMap CategoryMap { get; set; }
    }
}
