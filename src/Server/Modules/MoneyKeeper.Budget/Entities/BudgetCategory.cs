using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Entities
{
    public class BudgetCategory
    {
        public int Id { get; set; } 
        public string Group { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
    }
}
