using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class BudgetCategoryPositionGenerator
    {
        public IReadOnlyCollection<CategorySpreadsheetMap> Generate(IReadOnlyCollection<BudgetCategory> categories, IReadOnlyCollection<string> rawData, int firstRowNumber)
        {
            var categorySpreadsheetMap = new List<CategorySpreadsheetMap>();
            
            throw new NotImplementedException();
        }
    }
}
