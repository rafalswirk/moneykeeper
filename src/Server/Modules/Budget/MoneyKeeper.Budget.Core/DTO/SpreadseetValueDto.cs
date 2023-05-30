using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DTO
{
    public record SpreadsheetValueDto(string Spreadsheet, string Row, string Column, string Value);
}
