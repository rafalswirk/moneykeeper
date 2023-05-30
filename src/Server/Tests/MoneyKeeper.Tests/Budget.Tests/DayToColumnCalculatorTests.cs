using MoneyKeeper.Budget.Core.Services.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.UnitTests.Budget.Tests
{
    public class DayToColumnCalculatorTests
    {
        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "J")]
        [InlineData(3, "K")]
        [InlineData(19, "AA")]
        [InlineData(31, "AM")]
        public void CalculateColumn_DayNumber_ReturnsSpreadseetColumn(int day, string spreadsheetColumn)
        {
            var calculator = new DayToColumnCalculator();
            
            var result = calculator.CalculateColumn(day);

            Assert.Equal(spreadsheetColumn, result);
        }
    }
}
