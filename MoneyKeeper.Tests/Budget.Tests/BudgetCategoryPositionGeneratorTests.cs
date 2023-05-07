using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Console.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Tests.Budget.Tests
{
    public class BudgetCategoryPositionGeneratorTests
    {
        [Fact]
        public void Generate_SpreadsheetRawData_ReturnsBudgetCategoryPosition()
        {
            int firstRowNumber = 10;
            var input = new List<string>
            {
                "Telekomunikacja",
                "Telefon 1",
                "TV",
                ".",
                " ",
                "Opieka zdrowotna",
                "Lekarz",
                "Inne",
                ""
            };
            var categories = new List<BudgetCategory>()
            {
                new BudgetCategory { Category = "Telefon 1", Group = "Telekomunikacja"},
                new BudgetCategory { Category = "TV", Group = "Telekomunikacja"},
                new BudgetCategory { Category = "Lekarz", Group = "Opieka zdrowotna"},
                new BudgetCategory { Category = "Inne", Group = "Opieka zdrowotna"}
            };
            var generator = new BudgetCategoryPositionGenerator();
            var mappings = generator.Generate(categories, input, firstRowNumber);

            Assert.NotNull(mappings);
            Assert.Equal(4, mappings.Count);
            Assert.True(mappings.Single(m => m.Category.Category == "Inne" && m.Category.Group == "Opieka zdrowotna").Row == "17");    
            Assert.True(mappings.Single(m => m.Category.Category == "TV").Row == "12");    
        }
    }
}
