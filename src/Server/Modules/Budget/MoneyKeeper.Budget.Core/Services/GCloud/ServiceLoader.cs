using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public class ServiceLoader
    {
        private readonly string _credentialFilePath;
        private readonly string _spreadsheetId;

        public ServiceLoader(string credentialFilePath, string spreadsheetId)
        {
            _credentialFilePath = credentialFilePath;
            _spreadsheetId = spreadsheetId;
        }
        public (SheetsService, Spreadsheet) LoadService()
        {
            GoogleCredential credential = GoogleCredential.FromFile(_credentialFilePath).CreateScoped(SheetsService.Scope.Spreadsheets);

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Books API Sample",
            });

            var spreadsheet = service.Spreadsheets.Get(_spreadsheetId).Execute();
            return (service, spreadsheet);
        }
    }
}
