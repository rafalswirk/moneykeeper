using MoneyKeeper.OCR.GCloud;
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

        [Fact]
        public void ParseGCloudData()
        {
            var parser = new TextAnnotationParser();
            var annotations = parser.Parse(_jsonDocument);

        }
    }
}