using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    internal interface ICategorySpreadsheetMapRepository
    {
        Task AddAsync(CategorySpreadsheetMap category);
        Task DeleteAsync(CategorySpreadsheetMap category);
        Task<IReadOnlyCollection<CategorySpreadsheetMap>> BrowseAsync();
        Task<CategorySpreadsheetMap> GetAsync(int id);
        Task UpdateCategoryAsync(CategorySpreadsheetMap category);
    }
}
