using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Console.GCloud;

namespace MoneyKeeper.Budget.API.Controllers

{
    [ApiController]
    [Route("api/receipt/analysis")]
    public class ReceipAnalysisController : ControllerBase
    {
        private readonly ReceiptAnalysis _receiptAnalysis;

        public ReceipAnalysisController(ReceiptAnalysis receiptAnalysis)
        {
            _receiptAnalysis = receiptAnalysis;
        }

        [HttpPost]
        public async Task<IActionResult> MakeAnalysis(int id)
        {
            var dto = await _receiptAnalysis.MakeAnalysis(id);
            if(dto != null)
                return Ok(dto);
            return StatusCode(500);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAnalysis(int id)
        {
            throw new NotImplementedException();
        }
    }
}
