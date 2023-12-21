using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Entities
{
    public class SpreadsheetModificationHistory
    {
        [Key]
        public int Id { get; set; }
        public string SheetName { get; set; }
        [StringLength(10)]
        public string Row { get; set; }
        [StringLength(10)]
        public string Column { get; set; }
        public string Value { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
