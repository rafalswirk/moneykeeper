using MoneyKeeper.Console.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Tests.Budget.Tests
{
    public class SpreadsheetDataEditorTests
    {
        [Theory]
        [InlineData("", "1.2", "1.2")]
        [InlineData("1", "1.2", "=1+1.2")]
        [InlineData("=1", "1.2", "=1+1.2")]
        [InlineData("=1+2", "1.2", "=1+2+1.2")]
        public void Add_StringValueToContent_ReturnsFormulaWithAllData(string cellContent, string value, string expectedFormula)
        {
            var editor = new SpreadsheetDataEditor();

            var formula = editor.Add(cellContent, value);

            Assert.True(formula.Equals(expectedFormula));
        }
    }
}