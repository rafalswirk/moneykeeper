using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace MoneyKeeper.Tests
{
    public class GCloudOcrDataReaderTests
    {
        [Fact]
        public void FindDataInGCloudJsonFile()
        {
            int foundElements = 0;
            var jsonDocument = JsonDocument.Parse(File.OpenRead(@"TestData/BillsOfSale/GCloud/1_gcloud.json"));
            
            var textAnnotations = jsonDocument
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
    }
}