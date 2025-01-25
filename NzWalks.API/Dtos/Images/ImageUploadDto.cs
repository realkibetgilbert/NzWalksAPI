using System.ComponentModel.DataAnnotations;

namespace NzWalks.API.Dtos.Images
{
    public class ImageUploadDto
    {

        
        public  IFormFile File { get; set; }
        
        public string FileName { get; set; }

        public string? FileDecription {  get; set; }
    }
}
