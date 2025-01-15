using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Services.SqlServerImplementations
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext _nzWalksDbContext;

        public RegionRepository(NzWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await _nzWalksDbContext.Regions.AddAsync(region);

            await _nzWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(long id)
        {
            bool exists = await _nzWalksDbContext.Regions.AnyAsync(region => region.Id == id);

            if (!exists)
            {
                return null; // Return null if the Region doesn't exist
            }
            // Retrieve the existing Region
            var existingRegion = await _nzWalksDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            // Remove the Region
            _nzWalksDbContext.Regions.Remove(existingRegion);

            // Save changes
            await _nzWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var pagedData = await _nzWalksDbContext.Regions
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return pagedData;

        }

        public async Task<Region?> GetByIdAsync(long id)
        {
            return await _nzWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _nzWalksDbContext.Regions.CountAsync();
        }

        public async Task<Region?> UpdateAsync(long id, Region region)
        {
            var existingRegion = await _nzWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.ImageUrl = region.ImageUrl;

            await _nzWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
