using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public class BudgetCategoriesGenerator
    {
        private IGoogleDocsEditor _googleDocsEditor;
        private readonly SpreadsheetSettings _categoriesSettings;

        public BudgetCategoriesGenerator(IGoogleDocsEditor googleDocsEditor, SpreadsheetSettings categoriesSettings)
        {
            _googleDocsEditor = googleDocsEditor;
            _categoriesSettings = categoriesSettings;
        }

        public async Task<IReadOnlyCollection<BudgetCategory>> GenerateAsync(string range)
        {
            var rawData = await _googleDocsEditor.GetValuesRangeAsync(_categoriesSettings.CategorySheetName, range);
            var categories = new List<BudgetCategory>();
            var groupBy = string.Empty;
            var assignNewGroup = true;
            foreach (var rawItem in rawData)
            {
                if (rawItem == ".")
                    continue;

                if (rawItem == string.Empty || rawItem == " ")
                {
                    assignNewGroup = true;
                    continue;
                }

                if(assignNewGroup)
                {
                    groupBy = rawItem;
                    assignNewGroup = false;
                    continue;
                }

                categories.Add(new BudgetCategory
                {
                    Group = groupBy,
                    Category = rawItem,
                    Comment = string.Empty
                });
            }

            return categories;
        }
    }
}
