using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Infrastructure.EntityConfigurations;
using System;

namespace Socialite.Infrastructure.Data
{
    public class SocialiteDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Post> Posts { get; set; }
        
        public DbSet<PostState> PostStates { get; set; }
        
        public DbSet<Album> Albums { get; set; }
        
        public DbSet<Photo> Photos { get; set; }

        public SocialiteDbContext(DbContextOptions<SocialiteDbContext> contextOptions, IMediator mediator)
            : base(contextOptions)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StatusEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PostStateEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync();

            return true;
        }

        public void ApplyMigrations(SocialiteDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}