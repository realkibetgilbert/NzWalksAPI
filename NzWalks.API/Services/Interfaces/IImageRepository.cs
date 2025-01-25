using NzWalks.MODEL;

namespace NzWalks.API.Services.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
