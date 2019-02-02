using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.Data
{
    public class StatusContext : DbContext, IUnitOfWork
    {
        private IMediator _mediator;

        public DbSet<Status> Statuses { get; set; }

        public StatusContext(DbContextOptions<StatusContext> contextOptions, IMediator mediator)
            : base(contextOptions)
        {
            this._mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().Ignore(s => s.Events);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Mood = "🤙", Text = "Feelin' fine." },
                new Status { Id = 2, Mood = "☹️", Text = "Ah, man, no good..." }
            );
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