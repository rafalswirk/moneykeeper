using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.DTO;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    public interface IBudgetCategoryRepository
    {
        Task AddAsync(BudgetCategory category);
        Task DeleteAsync(BudgetCategory category);
        Task<IReadOnlyCollection<BudgetCategory>> BrowseAsync();
        Task<BudgetCategory> GetAsync(int id);
        Task UpdateCategoryAsync(BudgetCategory category);

    }
}
