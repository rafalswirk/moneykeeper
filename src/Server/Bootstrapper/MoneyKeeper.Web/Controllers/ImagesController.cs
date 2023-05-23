using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{

    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly string _imageDirectoryPath;

        public ImagesController()
        {
            // Set the directory where images will be saved
            _imageDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(_imageDirectoryPath))
            {
                Directory.CreateDirectory(_imageDirectoryPath);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            try
            {
                // Generate a unique filename for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Create the full path to the image file
                var filePath = Path.Combine(_imageDirectoryPath, fileName);

                // Save the image to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the URL of the saved image
                var imageUrl = $"{Request.Scheme}://{Request.Host}/api/images/{fileName}";
                return Ok(new { imageUrl });
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
