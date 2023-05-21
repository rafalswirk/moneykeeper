﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class GoogleDocsEditor : IGoogleDocsEditor
    {
        SheetsService _sheetsService;
        Spreadsheet _spreadsheet;
        private readonly SpreadsheetDataEditor _dataEditor;

        public GoogleDocsEditor(SpreadsheetDataEditor dataEditor)
        {
            _dataEditor = dataEditor;
        }

        public async Task Init()
        {
            var loader = new ServiceLoader();

            var sheetsService = await loader.LoadService();

            (_sheetsService, _spreadsheet) = await GetSpreadSheet();
        }

        public void AddValueToGoogleDocs(string sheet, string row, string column, string value)
        {
            var cellValue = GetValueFromCell(sheet, column, row);
            var newValue = _dataEditor.Add(cellValue, value);
            WriteCellValue(sheet, column, row, newValue);
        }

        public IEnumerable<string> GetValuesRange(string range)
        {
            var sheet = _spreadsheet.Sheets.Single(s => s.Properties.Title == "Wzorzec kategorii");
            string sheetRange = $"{sheet.Properties.Title}!B35:B177";

            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheet.SpreadsheetId, sheetRange);
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMULA;
            var valueRange = request.Execute();
            var returnValue = valueRange.Values.Select(v => v.FirstOrDefault() ?? string.Empty).Cast<string>().ToList();
            return returnValue;
        }

        private string GetValueFromCell(string sheetName, string column, string row)
        {
            var cellCoordinates = $"{column}{row}";
            var sheet = _spreadsheet.Sheets.Single(s => s.Properties.Title == sheetName);
            string sheetRange = $"{sheet.Properties.Title}!{cellCoordinates}";

            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheet.SpreadsheetId, sheetRange);
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMULA;
            var valueRange = request.Execute();
            var returnValue = valueRange.Values.Select(v => v.FirstOrDefault() ?? string.Empty).Cast<string>().Single();
            return returnValue;
        }

        private void WriteCellValue(string sheetName, string column, string row, string value)
        {
            var cellCoordinates = $"{column}{row}";
            var january = _spreadsheet.Sheets.Single(s => s.Properties.Title == sheetName);
            string range = $"{january.Properties.Title}!{cellCoordinates}";
            var dataToSend = new List<object>() { value };

            var updateRequest = new ValueRange()
            {
                MajorDimension = "COLUMNS",
                Range = range,
                Values = new List<IList<object>>() { dataToSend }
            };

            var request = _sheetsService.Spreadsheets.Values.Update(updateRequest, _spreadsheet.SpreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            request.Execute();
        }

        private async Task<(SheetsService, Spreadsheet)> GetSpreadSheet()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var clientSecrets = await GoogleClientSecrets.FromStreamAsync(stream);
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets.Secrets,
                    new[] { SheetsService.Scope.Spreadsheets },
                    "user", CancellationToken.None, new FileDataStore("Development"));
            }

            // Create the service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Books API Sample",
            });

            var sheetId = "1U_ntsBx82SGhshgs1aR09zAyCB_L0EcJlMh__xAIn9w";
            var spreadsheet = service.Spreadsheets.Get(sheetId).Execute();
            return (service, spreadsheet);
        }
    }
}
