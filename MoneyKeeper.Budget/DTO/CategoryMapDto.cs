using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DTO
{
    internal class CategoryMapDto
    {
        public int Id { get; set; }
        [StringLength(4)]
        public string Row { get; set; }
        [StringLength(4)]
        public string Column { get; set; }
        public BudgetCategoryDto Category { get; set; }
    }
}
