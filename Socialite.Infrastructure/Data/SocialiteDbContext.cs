using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;
using Socialite.Domain.AggregateModels.AlbumAggregate;
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
            SetupEntityModel<Status>(modelBuilder);

            SetupEntityModel<Post>(modelBuilder);

            SetupEntityModel<Album>(modelBuilder);
            modelBuilder.Entity<Album>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Album>().HasMany(a => a.Photos).WithOne();

            SetupEntityModel<Photo>(modelBuilder);
            modelBuilder.Entity<Photo>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Photo>().Property(a => a.Width).IsRequired();
            modelBuilder.Entity<Photo>().Property(a => a.Height).IsRequired();

            modelBuilder.Entity<Photo>().Property(a => a.Location).IsRequired().HasConversion(
                locationUri => locationUri.ToString(),
                locationString => new Uri(locationString)
            );

            var postStateSeedData = PostState.List().ToArray();
            modelBuilder.Entity<PostState>().HasData(postStateSeedData);
        }

        private void SetupEntityModel<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            const string DATETIME_NOW_FUNC = "CURRENT_TIMESTAMP(6)";

            var entityModel = modelBuilder.Entity<T>();

            entityModel.Ignore(m => m.Events);
            entityModel.Property(m => m.Id).ValueGeneratedOnAdd();
            entityModel.Property(m => m.CreatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC).ValueGeneratedOnAdd();
            entityModel.Property(m => m.UpdatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC).ValueGeneratedOnAddOrUpdate();
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync();

            return true;
        }
    }
}