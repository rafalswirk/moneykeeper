using MoneyKeeper.Transactions.OCR.GCloud;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace MoneyKeeper.Tests
{
    public class GCloudOcrDataReaderTests
    {
        private readonly JsonDocument _jsonDocument;

        public GCloudOcrDataReaderTests()
        {
            _jsonDocument = JsonDocument.Parse(File.OpenRead(@"TestData/BillsOfSale/GCloud/1_gcloud.json"));
        }

        [Fact]
        public void FindDataInGCloudJsonFile()
        {
            int foundElements = 0;

            var textAnnotations = _jsonDocument
                .RootElement
                .GetProperty("responses")
                .EnumerateArray()
                .First()
                .GetProperty("textAnnotations")
                .EnumerateArray();

            foreach (var annotation in textAnnotations)
            {
                if (annotation.TryGetProperty("description", out JsonElement description))
                {
                    if(description.ToString() == "SUMA")
                        foundElements++;
                }
            }

            Assert.Equal(2, foundElements);
        }

        [Theory]
        [InlineData(@"TestData/BillsOfSale/GCloud/SimpleData/Large_spacing.json", "second", "Second line")]
        [InlineData(@"TestData/BillsOfSale/GCloud/SimpleData/Small_spacing.json", "second", "Second line")]
        [InlineData(@"TestData/BillsOfSale/GCloud/SimpleData/Small_spacing_rotated.json", "second", "Second line")]
        [InlineData(@"TestData/BillsOfSale/GCloud/SimpleData/Simple_receipt.json", "pln", "SUMA PLN 3,14")]
        public void Parse_GCloudData_ExtractLineOfText(string jsonPath, string pattern, string expectedResult)
        {
            var jsonDocument = JsonDocument.Parse(File.OpenRead(jsonPath));

            var parser = new TextAnnotationParser();
            var annotations = parser.Parse(jsonDocument, pattern);

            Assert.Equal(1, annotations.Count);
            Assert.True(annotations.First().Description.Equals(expectedResult));
        }
    }
}