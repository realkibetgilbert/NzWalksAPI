using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.Interfaces
{
    public interface IwalkRepository
    {

        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(PaginationFilter paginationFilter);
        Task<Walk?> GetByIdAsync(long id);
        Task<Walk?> UpdateAsync(long id, Walk region);
        Task<Walk?> DeleteAsync(long id);
        Task<int> GetTotalCountAsync();
    }
}
