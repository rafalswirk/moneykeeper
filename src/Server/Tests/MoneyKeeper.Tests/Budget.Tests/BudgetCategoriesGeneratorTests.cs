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
        public void Generate_SpreadsheetRawData_ReturnsCategoryCollection()
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
            var mock = new Mock<IGoogleDocsEditor>();
            mock.Setup(m => m.GetValuesRange(It.Is<string>(s => s.Equals("Wzorzec kategorii")), It.IsAny<string>())).Returns(input);
            var generator = new BudgetCategoriesGenerator(mock.Object);
            
            var categories = generator.Generate("B35:B177");

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
