using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DTO
{
    public class BudgetCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Group { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; }
        public string Comment { get; set; }
    }
}
