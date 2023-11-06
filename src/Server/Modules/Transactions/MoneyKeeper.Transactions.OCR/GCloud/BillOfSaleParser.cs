using MoneyKeeper.Transactions.OCR.GCloud.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.OCR.GCloud
{
    public class BillOfSaleParser
    {
        public Receipt Parse(string gCloudData)
        {
            var textAnnotationParser = new TextAnnotationParser();
            var jsonData = JsonDocument.Parse(gCloudData);
            var rawTaxNumber = textAnnotationParser.Parse(jsonData, "nip");
            var rawDate = textAnnotationParser.Parse(jsonData, "23");
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
                if (text.Description.Contains("PTU"))
                    continue;
                var separator = text.Description.Contains(",") ? "," : ".";
                var regex = new Regex($"\\d+({separator}\\d+)?");
                var match = regex.Match(text.Description.ToLower());


                if (match.Success)
                {
                    string commaSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    var value = match.Value.Replace(",", commaSeparator).Replace(".", commaSeparator);
                    if (double.TryParse(value, out double result))
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

            foreach (var text in textData)
            {
                var cleanText = text.Description.ToLower().Replace("nip", "").Replace(":", "").Replace("-", "").Replace(" ", "");
                var regex = new Regex(@"\d{10}");
                var match = regex.Match(cleanText);
                if (match.Success)
                {
                    return match.Value;
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

                regex = new Regex(@"\d{2}-\d{2}-\d{4}");
                match = regex.Match(text.Description);
                if (match.Success)
                {
                    if (DateOnly.TryParseExact(match.Value.Replace("r", " "), "dd-MM-yyyy", out DateOnly result))
                    {
                        return result;
                    }
                }

                regex = new Regex(@"\d{2}r\d{2}.\d{2}");
                match = regex.Match(text.Description);
                if (match.Success)
                {
                    if (DateOnly.TryParseExact(match.Value.Replace("r", " "), "yy MM.dd", out DateOnly result))
                    {
                        return result;
                    }
                }
            }
            return null;
        }

    }
}
