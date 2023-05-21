using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Entities
{
    public class SheetToMonthMap
    {
        public int Id { get; set; }
        public byte Month { get; set; }
        public string SheetName { get; set; }
    }
}
