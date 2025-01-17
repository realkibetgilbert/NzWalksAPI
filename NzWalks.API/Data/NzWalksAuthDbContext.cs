using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.API.Data
{
    public class NzWalksAuthDbContext : IdentityDbContext
    {
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8ad62398-7eab-49c4-901f-6ee30d732e9f";
            var writerRoleId = "9240b7c8-4540-4d41-8859-79586e6f253b";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName ="Reader".ToUpper(),

                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName ="Writer".ToUpper(),

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
