using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Entities
{
    public class Spreadsheets
    {
        public int Id { get; set; }
        public string SpreadsheetKey { get; set; }
        public int Year { get; set; }
    }
}
