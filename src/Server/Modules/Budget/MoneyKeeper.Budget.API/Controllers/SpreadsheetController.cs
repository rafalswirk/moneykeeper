using Microsoft.AspNetCore.Mvc;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/spreadsheet")]
    public class SpreadsheetController: ControllerBase
    {
        public SpreadsheetController()
        {
                
        }


        [HttpGet]
        public IActionResult GetValue()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult SendValue()
        { 
            return BadRequest(); 
        }
    }
}
