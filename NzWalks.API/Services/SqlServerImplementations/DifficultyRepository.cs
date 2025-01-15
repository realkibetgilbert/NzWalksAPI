using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.SqlServerImplementations
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly NzWalksDbContext _nzWalksDbContext;

        public DifficultyRepository(NzWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await _nzWalksDbContext.Difficulties.AddAsync(difficulty);

            await _nzWalksDbContext.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(long id)
        {
            bool exists = await _nzWalksDbContext.Difficulties.AnyAsync(region => region.Id == id);

            if (!exists)
            {
                return null;
            }
            var difficulty = await _nzWalksDbContext.Difficulties.FirstOrDefaultAsync(region => region.Id == id);

            if (difficulty == null)
            {
                return null;
            }

            _nzWalksDbContext.Difficulties.Remove(difficulty);

            await _nzWalksDbContext.SaveChangesAsync();

            return difficulty;
        }

        public async Task<List<Difficulty>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var pagedData = await _nzWalksDbContext.Difficulties
               .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
               .Take(paginationFilter.PageSize)
               .ToListAsync();

            return pagedData;
        }

        public async Task<Difficulty?> GetByIdAsync(long id)
        {
            return await _nzWalksDbContext.Difficulties.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {

            return await _nzWalksDbContext.Difficulties.CountAsync();
        }

        public async Task<Difficulty?> UpdateAsync(long id, Difficulty difficulty)
        {
            var existingDifficulty = await _nzWalksDbContext.Difficulties.FirstOrDefaultAsync(r => r.Id == id);

            if (existingDifficulty == null)
            {
                return null;
            }

            existingDifficulty.Name = difficulty.Name;

            await _nzWalksDbContext.SaveChangesAsync();

            return existingDifficulty;
        }
    }
}
