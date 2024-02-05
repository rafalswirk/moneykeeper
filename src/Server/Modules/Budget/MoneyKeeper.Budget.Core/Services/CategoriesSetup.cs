using Google.Apis.Sheets.v4.Data;
using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Repositories;
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
        private readonly SpreadsheetSettings _categoriesSettings;
        private readonly ISpreadsheetRepository _spreadsheetRepository;

        public CategoriesSetup(IBudgetCategoryRepository budgetCategoryRepository,
                               ICategorySpreadsheetMapRepository spreadsheetMapRepository,
                               IGoogleDocsEditor editor,
                               BudgetCategoriesGenerator categoriesGenerator,
                               BudgetCategoryPositionGenerator categoryPositionGenerator,
                               SpreadsheetSettings categoriesSettings,
                               ISpreadsheetRepository spreadsheetRepository)
        {
            _budgetCategoryRepository = budgetCategoryRepository;
            _spreadsheetMapRepository = spreadsheetMapRepository;
            _editor = editor;
            _categoriesGenerator = categoriesGenerator;
            _categoryPositionGenerator = categoryPositionGenerator;
            _categoriesSettings = categoriesSettings;
            _spreadsheetRepository = spreadsheetRepository;
        }
        public async Task MakeAsync(string spreadsheetKey, string range)
        {
            var categories = await _categoriesGenerator.GenerateAsync(spreadsheetKey, range);

            foreach (var category in categories)
            {
                await _budgetCategoryRepository.AddAsync(category);
            }
            var rawData = await _editor.GetValuesRangeAsync(spreadsheetKey, _categoriesSettings.CategorySheetName, range);

            var positionGenerator = new BudgetCategoryPositionGenerator();
            var positions = positionGenerator.Generate(categories, rawData.ToList(), _categoriesSettings.CategoryOffset);

            foreach (var position in positions)
            {
                await _spreadsheetMapRepository.AddAsync(position);
            }
        }
    }
}
