using MoneyKeeper.Budget.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    internal interface IBudgetCategoryRepository
    {
        Task AddCategoryAsync(string group, string category, string comment);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(int id, string group, string category, string comment);
        Task<IReadOnlyCollection<BudgetCategoryDto>> GetCategoriesAsync();
        Task<BudgetCategoryDto> GetCategoryAsync(int id);

        
    }
}
