using NzWalks.API.Data;
using NzWalks.API.Services.Interfaces;
using NzWalks.MODEL;

namespace NzWalks.API.Services.SqlServerImplementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NzWalksDbContext _nzWalksDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NzWalksDbContext nzWalksDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Image> UploadAsync(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.File.FileName} {image.FileExtension}");
            //upload image to local path,....
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            ///save changes to the database we the local path
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.File.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await _nzWalksDbContext.Images.AddAsync(image);
            await _nzWalksDbContext.SaveChangesAsync();
            return image;

        }
    }
}
