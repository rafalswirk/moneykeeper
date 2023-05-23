using MoneyKeeper.OCR.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Tests
{
    public class BillOfSaleParserTests
    {
        [Fact]
        public void Parse_GCloudOCRResult_ReturnsBillOfSaleData()
        {
            var parser = new BillOfSaleParser();
            var receipt = parser.Parse(File.ReadAllText(@"TestData/BillsOfSale/GCloud/1_gcloud.json"));

            Assert.NotNull(receipt);
            Assert.Equal("7791011327", receipt.TaxNumber);
            Assert.Equal(new DateOnly(2023, 2, 14), receipt.Date);
            Assert.Equal(56.90, receipt.Total);
        }
    }
}
