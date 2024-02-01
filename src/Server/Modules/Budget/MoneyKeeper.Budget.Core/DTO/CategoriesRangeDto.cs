using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DTO
{
    public record CategoriesRangeDto(string SpreadsheetKey, string From, string To);
}
