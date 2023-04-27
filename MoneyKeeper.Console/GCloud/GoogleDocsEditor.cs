using Google.Apis.Auth.OAuth2;
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
    internal class GoogleDocsEditor
    {
        public async Task AddValueToGoogleDocs(string token, string projectId)
        {
            var loader = new ServiceLoader();

            var sheetsService = await loader.LoadService();

            var (service, spreadsheet) = await GetSpreadSheet();
            GetValueFromCell(service, spreadsheet);
            WriteValueToCell(service, spreadsheet);
        }

        private string GetValueFromCell(SheetsService service, Spreadsheet spreadsheet)
        {
            var january = spreadsheet.Sheets.Single(s => s.Properties.Title == "Styczeń");
            string range = $"{january.Properties.Title}!I58";
            
            var request = service.Spreadsheets.Values.Get(spreadsheet.SpreadsheetId, range);
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMULA;
            var valueRange = request.Execute();
            var returnValue = valueRange.Values.First().Single().ToString();
            return returnValue;
        }

        private static void WriteValueToCell(SheetsService service, Spreadsheet spreadsheet)
        {
            var january = spreadsheet.Sheets.Single(s => s.Properties.Title == "Styczeń");
            string range = $"{january.Properties.Title}!J58";
            var value = new List<object>() { "=777+311", "11" };

            var updateRequest = new ValueRange()
            {
                MajorDimension = "COLUMNS",
                Range = range,
                Values = new List<IList<object>>() { value }
            };
            // Execute the update request
            var request = service.Spreadsheets.Values.Update(updateRequest, "1U_ntsBx82SGhshgs1aR09zAyCB_L0EcJlMh__xAIn9w", range);
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
