using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Transactions.Core.Data;
using MoneyKeeper.Transactions.Core.Storage;
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

        public ReceiptStorageController(DataDirectoriesWrapper dataDirectories, RecepitStorage receiptStorage)
        {
            _receiptStorage = receiptStorage;

            _imageDirectoryPath = dataDirectories.ReceiptImagesPath;
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
        public IActionResult GetImageUrls()
        {
            // Get the list of image files
            var imageFiles = Directory.GetFiles(_imageDirectoryPath);
            // Create a list of image URLs
            var imageUrls = new List<string>();
            foreach (var imageFile in imageFiles)
            {
                var fileName = Path.GetFileName(imageFile);
                var imageUrl = $"{Request.Scheme}://{Request.Host}/api/images/{fileName}";
                imageUrls.Add(imageUrl);
            }
            return Ok(imageUrls);
        }

        [HttpGet("isalive")]
        public IActionResult IsAlive()
        {
            return Ok();
        }
    }

}
