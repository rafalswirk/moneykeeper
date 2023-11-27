using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Transactions.Core.Services;

namespace MoneyKeeper.Transactions.API.Controllers

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
        public async Task<IActionResult> MakeAnalysis([FromBody] int id)
        {
            var dto = await _receiptAnalysis.MakeAnalysis(id);
            if (dto != null)
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
