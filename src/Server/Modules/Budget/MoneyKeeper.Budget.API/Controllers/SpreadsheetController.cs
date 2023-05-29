using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/spreadsheet")]
    public class SpreadsheetController: ControllerBase
    {
        private readonly GoogleDocsEditor _googleDocsEditor;
        private readonly CategoriesSetup _categoriesSetup;
        private readonly BudgetCategoriesGenerator _categoriesGenerator;

        public SpreadsheetController(GoogleDocsEditor googleDocsEditor, CategoriesSetup categoriesSetup)
        {
            _googleDocsEditor = googleDocsEditor;
            _categoriesSetup = categoriesSetup;
        }


        [HttpGet]
        public IActionResult GetValue()
        {
            throw new NotImplementedException();
        }

        [HttpPost("sendvalue")]
        public IActionResult SendValue([FromBody]SpreadsheetValueDto dto)
        {
            _googleDocsEditor.AddValueToGoogleDocsAsync(dto.Spreadsheet, dto.Row, dto.Column, dto.Value);
            return Ok(); 
        }

        [HttpPost("generatecategories")]
        public async Task<IActionResult> GenerateCategories([FromBody] CategoriesRangeDto dto)
        {
            await _categoriesSetup.MakeAsync($"{dto.From}:{dto.To}");
            return Ok();
        }

        [HttpGet("getcategories")]
        public IActionResult GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
