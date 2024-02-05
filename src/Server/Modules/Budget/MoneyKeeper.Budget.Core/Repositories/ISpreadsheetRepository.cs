using MoneyKeeper.Budget.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Repositories
{
    public interface ISpreadsheetRepository
    {
        Task<IReadOnlyCollection<Spreadsheet>> BrowseAsync();
        Task<Spreadsheet> GetSpreadsheetByYear(int year);
    }
}
