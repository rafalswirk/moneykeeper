using MoneyKeeper.OCR.GCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyKeeper.OCR.GCloud
{
    public class BillOfSaleParser
    {
        public Receipt Parse(string gCloudData)
        {
            var textAnnotationParser = new TextAnnotationParser();
            var jsonData = JsonDocument.Parse(gCloudData);
            //var parsertextAnnotationParser.Parse(jsonData, "nip");
            throw new NotImplementedException();
        }
    }
}
