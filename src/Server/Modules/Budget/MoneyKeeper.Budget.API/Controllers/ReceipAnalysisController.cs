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
        public async Task<IActionResult> Analyse(int id)
        {
            var analysisResultDto = await _receiptAnalysis.Analysis(id);
            return Ok(analysisResultDto);
        }
    }
}
