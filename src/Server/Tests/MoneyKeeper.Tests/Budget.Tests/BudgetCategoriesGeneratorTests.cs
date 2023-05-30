using MoneyKeeper.Budget.Core.Data;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Console.GCloud;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Tests.Budget.Tests
{
    public class BudgetCategoriesGeneratorTests
    {
        [Fact]
        public async Task Generate_SpreadsheetRawData_ReturnsCategoryCollection()
        {
            var input = new List<string>
            { 
                "Telekomunikacja",
                "Telefon 1",
                "Telefon 2",
                "TV",
                "Internet",
                "Inne",
                ".",
                ".",
                ".",
                ".",
                ".",
                " ",
                "Opieka zdrowotna",
                "Lekarz",
                "Badania",
                "Lekarstwa",
                "Inne",
                ""
            };
            var spreadsheetSettings = new SpreadsheetSettings("Wzorzec kategorii", 79);
            var mock = new Mock<IGoogleDocsEditor>();
            mock.Setup(m => m.GetValuesRangeAsync(It.Is<string>(s => s.Equals(spreadsheetSettings.CategorySheetName)), It.IsAny<string>())).ReturnsAsync(input);
            var generator = new BudgetCategoriesGenerator(mock.Object, spreadsheetSettings);
            
            var categories = await generator.GenerateAsync("B35:B177");

            Assert.NotNull(categories);
            Assert.Equal(9, categories.Count);
            Assert.Equal(2, categories.Select(c => c.Group).Distinct().Count());
            Assert.Contains(categories, c => c.Group.Equals("Telekomunikacja"));
            Assert.Contains(categories, c => c.Group.Equals("Opieka zdrowotna"));
            Assert.Equal(5, categories.GroupBy(c => c.Group).First().Count());
            Assert.Equal(4, categories.GroupBy(c => c.Group).Last().Count());
            Assert.DoesNotContain(categories.Select(c => c.Category), c => string.IsNullOrEmpty(c));
        }
    }
}
