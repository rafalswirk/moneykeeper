using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Entities
{
    public class CategorySpreadsheetMap
    {
        public int Id { get; set; }
        public string Row { get; set; }
        public string Column { get; set; }
        public BudgetCategory Category  { get; set; }
    }
}
