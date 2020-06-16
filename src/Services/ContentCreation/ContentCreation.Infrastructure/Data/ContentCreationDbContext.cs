using System.Linq;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Data
{
    public class ContentCreationDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostState> PostStates { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public ContentCreationDbContext(
            DbContextOptions<ContentCreationDbContext> contextOptions,
            IMediator mediator
        ) : base(contextOptions)
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

            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await base.SaveChangesAsync();

            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

        public void ApplyMigrations(ContentCreationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}