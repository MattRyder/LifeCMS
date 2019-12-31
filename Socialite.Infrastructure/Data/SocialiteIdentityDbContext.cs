using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Socialite.Infrastructure.Identity;

namespace Socialite.Infrastructure.Data
{
    public class SocialiteIdentityDbContext : IdentityDbContext<SocialiteIdentityUser>
    {
        public SocialiteIdentityDbContext(DbContextOptions<SocialiteIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SocialiteIdentityUser>().ToTable("SocialiteIdentityUser");
            builder.Entity<SocialiteIdentityRole>().ToTable("SocialiteIdentityRole");
        }
    }
}
