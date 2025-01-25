using Microsoft.EntityFrameworkCore;
using NzWalks.MODEL;

namespace NzWalks.API.Data
{
    public class NzWalksDbContext:DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContextOptions):base (dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


    }
}
