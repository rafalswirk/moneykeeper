using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
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

        public ServiceLoader(string credentialFilePath)
        {
            _credentialFilePath = credentialFilePath;
        }
        public SheetsService LoadService()
        {
            GoogleCredential credential = GoogleCredential.FromFile(_credentialFilePath).CreateScoped(SheetsService.Scope.Spreadsheets);

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Books API Sample",
            });

            
            return service;
        }
    }
}
