using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.SqlServerImplementations
{
    public class WalkRepository : IwalkRepository
    {
        private readonly NzWalksDbContext _nzWalksDbContext;

        public WalkRepository(NzWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _nzWalksDbContext.Walks.AddAsync(walk);

            await _nzWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public  async Task<Walk?> DeleteAsync(long id)
        {
            bool exists = await _nzWalksDbContext.Walks.AnyAsync(walk => walk.Id == id);

            if (!exists)
            {
                return null;
            }
            var existingWalk = await _nzWalksDbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            _nzWalksDbContext.Walks.Remove(existingWalk);

            // Save changes
            await _nzWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var pagedData = await _nzWalksDbContext.Walks
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return pagedData;
        }

        public async Task<Walk?> GetByIdAsync(long id)
        {
            return await _nzWalksDbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _nzWalksDbContext.Walks.CountAsync();
        }

        public async  Task<Walk?> UpdateAsync(long id, Walk walk)
        {
            var existingWalk = await _nzWalksDbContext.Walks.FirstOrDefaultAsync(r => r.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.ImageUrl = walk.ImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm ;
            existingWalk.DifficultyId = walk.DifficultyId ;
            existingWalk.RegionId = walk.RegionId ;

            await _nzWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
