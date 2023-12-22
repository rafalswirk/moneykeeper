using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using MoneyKeeper.Budget.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public class GoogleDocsEditor : IGoogleDocsEditor
    {
        SheetsService _sheetsService;
        Spreadsheet _spreadsheet;
        private readonly ServiceLoader _serviceLoader;
        private readonly SpreadsheetDataEditor _dataEditor;
        private readonly ISpreadsheetModificationHistory _modificationHistory;
        private bool _isInitialized;

        public GoogleDocsEditor(ServiceLoader serviceLoader, SpreadsheetDataEditor dataEditor, ISpreadsheetModificationHistory modificationHistory)
        {
            _serviceLoader = serviceLoader;
            _dataEditor = dataEditor;
            _modificationHistory = modificationHistory;
        }

        public async Task AddValueToGoogleDocsAsync(string sheet, string row, string column, string value)
        {
            if(!_isInitialized)
                Init();
            var cellValue = GetValueFromCell(sheet, column, row);
            var newValue = _dataEditor.Add(cellValue, value);
            WriteCellValue(sheet, column, row, newValue);
            await _modificationHistory.RecordModificationAsync(new Entities.SpreadsheetModificationHistory 
            {
                Column = column,
                Row = row,
                SheetName = sheet,
                Value = value,
                ModificationDate = DateTime.Now,
            });
        }

        public async Task<IEnumerable<string>> GetValuesRangeAsync(string sheetName, string range)
        {
            if (!_isInitialized)
                Init();
            var sheet = _spreadsheet.Sheets.Single(s => s.Properties.Title == sheetName);
            string sheetRange = $"{sheet.Properties.Title}!{range}";

            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheet.SpreadsheetId, sheetRange);
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMULA;
            var valueRange = request.Execute();
            var returnValue = valueRange.Values.Select(v => v.FirstOrDefault() ?? string.Empty).Cast<string>().ToList();
            return returnValue;
        }

        private void Init()
        {
            (_sheetsService, _spreadsheet) = _serviceLoader.LoadService();
        }

        private string GetValueFromCell(string sheetName, string column, string row)
        {
            var cellCoordinates = $"{column}{row}";
            var sheet = _spreadsheet.Sheets.Single(s => s.Properties.Title == sheetName);
            string sheetRange = $"{sheet.Properties.Title}!{cellCoordinates}";

            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheet.SpreadsheetId, sheetRange);
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMULA;
            var valueRange = request.Execute();
            if (valueRange.Values is null)
                return string.Empty;
            var returnValue = valueRange.Values.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? string.Empty;
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
    }
}
