using Google.Apis.Sheets.v4.Data;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services
{
    public class CategoriesSetup
    {
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;
        private readonly ICategorySpreadsheetMapRepository _spreadsheetMapRepository;
        private readonly IGoogleDocsEditor _editor;
        private readonly BudgetCategoriesGenerator _categoriesGenerator;
        private readonly BudgetCategoryPositionGenerator _categoryPositionGenerator;

        public CategoriesSetup(IBudgetCategoryRepository budgetCategoryRepository,
                               ICategorySpreadsheetMapRepository spreadsheetMapRepository,
                               IGoogleDocsEditor editor,
                               BudgetCategoriesGenerator categoriesGenerator,
                               BudgetCategoryPositionGenerator categoryPositionGenerator)
        {
            _budgetCategoryRepository = budgetCategoryRepository;
            _spreadsheetMapRepository = spreadsheetMapRepository;
            _editor = editor;
            _categoriesGenerator = categoriesGenerator;
            _categoryPositionGenerator = categoryPositionGenerator;
        }
        public async Task Make(string range)
        {
            var categories = _categoriesGenerator.Generate(range);

            foreach (var category in categories)
            {
                await _budgetCategoryRepository.AddAsync(category);
            }
            var rawData = _editor.GetValuesRange("Wzorzec kategorii", range);

            var positionGenerator = new BudgetCategoryPositionGenerator();
            var positions = positionGenerator.Generate(categories, rawData.ToList(), 79);

            foreach (var position in positions)
            {
                await _spreadsheetMapRepository.AddAsync(position);
            }
        }
    }
}
