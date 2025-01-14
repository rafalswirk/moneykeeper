using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.DTO;
using MoneyKeeper.Transactions.Core.Services.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.API.Controllers
{

    [ApiController]
    [Route("api/receipt/storage")]
    public class ReceiptStorageController : ControllerBase
    {
        private readonly string _imageDirectoryPath;
        private readonly RecepitStorage _receiptStorage;
        private readonly ReceiptStorageReader _storageReader;
        private readonly ReceiptUpdate _receiptUpdate;
        private readonly ReceiptRemove _receiptRemove;

        public ReceiptStorageController(DataDirectoriesWrapper dataDirectories, RecepitStorage receiptStorage, ReceiptStorageReader storageReader, ReceiptUpdate receiptUpdate, ReceiptRemove receiptRemove)
        {
            _imageDirectoryPath = dataDirectories.ReceiptImagesPath;
            _receiptStorage = receiptStorage;
            _storageReader = storageReader;
            _receiptUpdate = receiptUpdate;
            _receiptRemove = receiptRemove;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var info = await _receiptStorage.SaveReceipt(file);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            // Create the full path to the requested image file
            var filePath = Path.Combine(_imageDirectoryPath, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Return the image file
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, "image/jpeg");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllInfo()
        {
            var info = await _storageReader.GetReceipts();
            return Ok(info);
        }

        [HttpPatch("spreadsheetEntered")]
        public async Task<IActionResult> ReceiptEnteredToSpreadsheet(int id)
        {
            await _receiptUpdate.SetReceiptEnteredToSpreadsheet(id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveReceipt(int id)
        {
            await _receiptRemove.Remove(id);
            return Ok();
        }

        [HttpGet("alive")]
        public IActionResult IsAlive()
        {
            return Ok();
        }
    }

}
