using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Socialite.Infrastructure.Data
{
    public class SocialiteDbContext : DbContext, IUnitOfWork
    {
        private IConfiguration _configuration;
        private IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostState> PostStates { get; set; }

        public DbSet<User> Users { get; set; }

        public SocialiteDbContext(DbContextOptions<SocialiteDbContext> contextOptions, IConfiguration configuration, IMediator mediator)
            : base(contextOptions)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupEntityModel<Status>(modelBuilder);
            SetupEntityModel<Post>(modelBuilder);
            SetupEntityModel<User>(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<Post>().HasOne(p => p.State).WithMany();

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

            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}