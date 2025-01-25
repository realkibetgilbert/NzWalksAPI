using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using NzWalks.API.Dtos.Images;
using NzWalks.API.Services.Interfaces;
using NzWalks.MODEL;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageRepository imageRepository,ILogger<ImageController> logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] ImageUploadDto imageUploadDto)
        {
                _logger.LogInformation("uploading image");
            _logger.LogError ("uploading image");
                _logger.LogWarning("uploading image");
            ValidateFileUpload(imageUploadDto);

            if (ModelState.IsValid)
            {
                var imageDomain = new Image
                {
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.FileName),
                    FileSizeInBytes = imageUploadDto.File.Length,
                    FileName = imageUploadDto.FileName,
                    FileDescription = imageUploadDto.FileDecription,
                };
                await _imageRepository.UploadAsync(imageDomain);
                return Ok(imageDomain);
            }

            return BadRequest();
        }

        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            var allowedExtensions = new string[] { ".png", ".jpeg", ".jpg" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("FILE", "unsupported file extension");

            }
            if (imageUploadDto.File.Length > 1048560)
            {
                ModelState.AddModelError("FILE", "file size more than 10mb");
            }
        }
    }
}
