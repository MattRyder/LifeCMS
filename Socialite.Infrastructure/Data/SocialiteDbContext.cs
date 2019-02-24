using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.Data
{
    public class SocialiteDbContext : DbContext, IUnitOfWork
    {
        private IConfiguration _configuration;
        private IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Post> Posts { get; set; }

        public SocialiteDbContext(DbContextOptions<SocialiteDbContext> contextOptions, IConfiguration configuration, IMediator mediator)
            : base(contextOptions)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string DATETIME_NOW_FUNC = "CURRENT_TIMESTAMP(6)";

            modelBuilder.Entity<Status>().Ignore(s => s.Events);
            modelBuilder.Entity<Status>().Property(s => s.CreatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC);

            modelBuilder.Entity<Post>().Ignore(p => p.Events);
            modelBuilder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC);
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}