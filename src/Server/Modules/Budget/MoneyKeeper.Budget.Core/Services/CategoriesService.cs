using MoneyKeeper.Budget.DTO;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class CategoriesService
    {
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;

        public CategoriesService(IBudgetCategoryRepository budgetCategoryRepository)
        {
            _budgetCategoryRepository = budgetCategoryRepository;
        }

        public async Task<IReadOnlyList<BudgetCategoryDto>> BrowseAsync()
        {
            var categories = await _budgetCategoryRepository.BrowseAsync();
            return categories.Select(Map<BudgetCategoryDto>).ToList();
        }

        private static T Map<T>(BudgetCategory map) where T : BudgetCategoryDto, new()
            => new T()
            {
                Id = map.Id,
                Category = map.Category,
                Group = map.Group,
                Comment = map.Comment,
            };
    }
}
