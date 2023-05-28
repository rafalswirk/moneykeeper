using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/spreadsheet")]
    public class SpreadsheetController: ControllerBase
    {
        private readonly GoogleDocsEditor _googleDocsEditor;

        public SpreadsheetController(GoogleDocsEditor googleDocsEditor)
        {
            _googleDocsEditor = googleDocsEditor;
        }


        [HttpGet]
        public IActionResult GetValue()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult SendValue([FromBody]SpreadsheetValueDto dto)
        {
            _googleDocsEditor.AddValueToGoogleDocs(dto.Spreadsheet, dto.Row, dto.Column, dto.Value);
            return Ok(); 
        }
    }
}
