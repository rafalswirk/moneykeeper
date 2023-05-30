using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud.Commands
{
    internal class LoadSpreadSheet : ICommand<Spreadsheet>
    {
        private readonly SheetsService _service;
        private readonly string _id;

        public LoadSpreadSheet(SheetsService service, string id)
        {
            _service = service;
            _id = id;
        }

        public Spreadsheet Execute()
        {
            return _service.Spreadsheets.Get(_id).Execute();
        }
    }
}
