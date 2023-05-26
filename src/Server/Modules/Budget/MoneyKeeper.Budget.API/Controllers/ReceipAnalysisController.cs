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
        private readonly ReceiptAnalysisReader _receiptAnalysisReader;

        public ReceipAnalysisController(ReceiptAnalysis receiptAnalysis, ReceiptAnalysisReader receiptAnalysisReader)
        {
            _receiptAnalysis = receiptAnalysis;
            _receiptAnalysisReader = receiptAnalysisReader;
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
            var dto = await _receiptAnalysisReader.ReadAnalysis(id);
            if (dto != null)
                return Ok(dto);
            return StatusCode(500);
        }
    }
}
