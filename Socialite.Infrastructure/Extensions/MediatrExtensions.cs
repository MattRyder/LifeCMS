using MediatR;
using Microsoft.EntityFrameworkCore;
using Socialite.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Socialite.Infrastructure.Exensions
{
    static class MediatrExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            var domainEvents = domainEntities
                .SelectMany(e => e.Events)
                .ToList();

            domainEntities.ToList().ForEach(e => e.ClearEvents());

            var eventTasks = domainEvents.Select(async (evnt) =>
            {
                await mediator.Publish(evnt);
            });

            await Task.WhenAll(eventTasks.ToArray());
        }
    }
}