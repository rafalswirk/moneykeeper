using Microsoft.AspNetCore.Mvc;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/spreadsheet")]
    public class SpreadsheetController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetValue()
        {
            return Ok();
        }
    }
}
