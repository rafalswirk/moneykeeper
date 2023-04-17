using MoneyKeeper.OCR.GCloud.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyKeeper.OCR.GCloud
{
    public class BillOfSaleParser
    {
        public Receipt Parse(string gCloudData)
        {
            var textAnnotationParser = new TextAnnotationParser();
            var jsonData = JsonDocument.Parse(gCloudData);
            var rawTaxNumber = textAnnotationParser.Parse(jsonData, "nip");
            var rawDate = textAnnotationParser.Parse(jsonData, "2023");
            var rawTotal = textAnnotationParser.Parse(jsonData, "suma");

            var taxNumber = ExtractTaxNumber(rawTaxNumber);
            var date = ExtractDate(rawDate);
            var total = ExtractSum(rawTotal);
            
            if (string.IsNullOrEmpty(taxNumber) || date == null || total == -1)
            {
                return null;
            }

            return new Receipt(taxNumber, date.Value, total);
        }

        private double ExtractSum(IReadOnlyCollection<TextAnnotation> textData)
        {
            foreach (var text in textData)
            {
                var regex = new Regex(@"(?<=SUMA PLN\s)\d+(,\d+)?");
                var match = regex.Match(text.Description);
                if (match.Success)
                {
                    if (double.TryParse(match.Value, out double result))
                        return result;
                }
            }

            return -1;
        }

        private string ExtractTaxNumber(IReadOnlyCollection<TextAnnotation> textData)
        {
            foreach (var text in textData)
            {
                var regex = new Regex(@"\d{3}-?\d{2}-?\d{2}-?\d{3}");
                var match = regex.Match(text.Description);
                if (match.Success)
                {
                    return match.Value.Replace("-", "");
                }
            }

            return string.Empty;
        }

        private DateOnly? ExtractDate(IReadOnlyCollection<TextAnnotation> textData)
        {
            foreach (var text in textData)
            {
                var regex = new Regex(@"\d{4}-\d{2}-\d{2}");
                var match = regex.Match(text.Description);
                if (match.Success)
                {
                    if (DateOnly.TryParse(match.Value, out DateOnly result))
                    {
                        return result;
                    }
                }
            }

            return null;
        }

    }
}
