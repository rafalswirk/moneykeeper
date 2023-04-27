using MoneyKeeper.Budget.DTO;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL.Repositories
{
    internal class BudgetCategoryRepository : IBudgetCategoryRepository
    {
        private readonly BudgetCategoryRepository _budgetCategory;

        public BudgetCategoryRepository(BudgetCategoryRepository budgetCategory)
        {
            _budgetCategory = budgetCategory;
        }

        public async Task AddCategoryAsync(string group, string category, string comment)
        {
           await _budgetCategory.AddCategoryAsync(group, category, comment);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _budgetCategory.DeleteCategoryAsync(id);
        }

        public async Task<IReadOnlyCollection<BudgetCategoryDto>> GetCategoriesAsync()
        {
            return await _budgetCategory.GetCategoriesAsync();
        }

        public async Task<BudgetCategoryDto> GetCategoryAsync(int id)
        {
            return await _budgetCategory.GetCategoryAsync(id);
        }

        public async Task UpdateCategoryAsync(int id, string group, string category, string comment)
        {
            await _budgetCategory.UpdateCategoryAsync(id, group, category, comment);
        }
    }
}
