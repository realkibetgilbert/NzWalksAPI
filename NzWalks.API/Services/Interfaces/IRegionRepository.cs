using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.Interfaces
{
    public interface IRegionRepository
    {
        Task<Region> CreateAsync(Region region);
        Task<List<Region>> GetAllAsync(PaginationFilter paginationFilter);
        Task<Region?> GetByIdAsync(long id);
        Task<Region?> UpdateAsync(long id, Region region);
        Task<Region?> DeleteAsync(long id);
        Task<int> GetTotalCountAsync();
    }
}
