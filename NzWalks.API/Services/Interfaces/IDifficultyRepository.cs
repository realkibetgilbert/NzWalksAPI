using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<List<Difficulty>> GetAllAsync(PaginationFilter paginationFilter);
        Task<Difficulty?> GetByIdAsync(long id);
        Task<Difficulty?> UpdateAsync(long id, Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(long id);
        Task<int> GetTotalCountAsync();
    }
}
