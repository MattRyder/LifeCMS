using Microsoft.EntityFrameworkCore;
using Socialite.Domain.Common;
using Socialite.Domain.Entities;
using System.Linq;

namespace Socialite.Infrastructure.Data
{
    public class SocialiteDbContext : DbContext
    {
        private IDomainEventDispatcher _dispatcher;

        public SocialiteDbContext(DbContextOptions<SocialiteDbContext> dbContextOptions, IDomainEventDispatcher eventDispatcher)
            : base(dbContextOptions)
        {
            this._dispatcher = eventDispatcher;
        }

        public DbSet<Status> Statuses { get; set; }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            // Select entities with events present:
            var eventfulEntities = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            // Handle all events this entity wants raising:
            foreach(var entity in eventfulEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach(var evnt in events)
                {
                    _dispatcher.Dispatch(evnt);
                }
            }

            return result;
        }
    }
}
