using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifeCMS.Services.Identity.Infrastructure.Data
{
    public class LifeCMSIdentityDbContext : IdentityDbContext<LifeCMSIdentityUser>
    {
        public LifeCMSIdentityDbContext(DbContextOptions<LifeCMSIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LifeCMSIdentityUser>().ToTable("LifeCMSIdentityUser");
            builder.Entity<LifeCMSIdentityRole>().ToTable("LifeCMSIdentityRole");
        }
    }
}
