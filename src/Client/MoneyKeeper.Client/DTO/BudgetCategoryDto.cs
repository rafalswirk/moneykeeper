using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.DTO
{
    public record BudgetCategoryDto(int Id, string Group, string Category);
}
