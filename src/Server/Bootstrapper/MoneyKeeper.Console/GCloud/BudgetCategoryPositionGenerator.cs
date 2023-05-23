using Microsoft.EntityFrameworkCore.Storage;
using MoneyKeeper.Budget.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class BudgetCategoryPositionGenerator
    {
        public IReadOnlyCollection<CategorySpreadsheetMap> Generate(IReadOnlyCollection<BudgetCategory> categories, List<string> rawData, int firstRowNumber)
        {
            var categorySpreadsheetMap = new List<CategorySpreadsheetMap>();
            var map = new List<CategorySpreadsheetMap>();
            var parentMap = new Dictionary<string, int>();

            for (int i = 0; i < rawData.Count; i++)
            {
                if (categories.Any(c => c.Group == rawData[i]))
                    parentMap.Add(rawData[i], i);
            }

            var rawParent = string.Empty;
            for (int i = 0; i < rawData.Count; i++)
            {
                if (string.IsNullOrEmpty(rawData[i]) || rawData[i] == "." || rawData[i] == " ")
                {
                    continue;
                }
                if (parentMap.ContainsKey(rawData[i]))
                {
                    rawParent = rawData[i];
                    continue;
                }

                var category = categories.Single(c => c.Group == rawParent && c.Category == rawData[i]);
                var mapItem = new CategorySpreadsheetMap
                {
                    Category = category,
                    Column = "0",
                    Row = (i + firstRowNumber).ToString()
                };

                map.Add(mapItem);


            }
            return map;
        }
    }
}
