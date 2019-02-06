using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.Data
{
    public class StatusContext : DbContext, IUnitOfWork
    {
        private IConfiguration _configuration;
        private IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public StatusContext(DbContextOptions<StatusContext> contextOptions, IConfiguration configuration, IMediator mediator)
            : base(contextOptions)
        {
            this._configuration = configuration;
            this._mediator = mediator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseMySql(_configuration["ConnectionStrings:Socialite"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string DATETIME_NOW_FUNC = "CURRENT_TIMESTAMP";

            modelBuilder.Entity<Status>().Ignore(s => s.Events);

            modelBuilder.Entity<Status>().Property(s => s.CreatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC);
            modelBuilder.Entity<Status>().Property(s => s.UpdatedAt).HasDefaultValueSql(DATETIME_NOW_FUNC);
        }

        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}