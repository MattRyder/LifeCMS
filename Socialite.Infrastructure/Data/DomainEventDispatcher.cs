using System;
using System.Collections;
using System.Collections.Generic;
using Socialite.Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Socialite.Infrastructure.Data
{
  public class DomainEventDispatcher : IDomainEventDispatcher
  {
    private IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
      this._serviceProvider = serviceProvider;
    }

    public void Dispatch(BaseEvent domainEvent)
    {
      Type handlerType = typeof(IEventHandle<>).MakeGenericType(domainEvent.GetType());
      Type wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

      IEnumerable handlers = (IEnumerable) _serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(domainEvent.GetType()));
      IEnumerable<DomainEventHandlerBase> eventHandlers = handlers.Cast<object>().Select(handler => (DomainEventHandlerBase)Activator.CreateInstance(wrapperType, true));

      // dispatch event to all registered event handlers:
      eventHandlers.ToList().ForEach(handler => handler.Handle(domainEvent));
    }

    private abstract class DomainEventHandlerBase
    {
      public abstract void Handle(BaseEvent domainEvent);
    }

    private class DomainEventHandler<T> : DomainEventHandlerBase where T : BaseEvent
    {
      private readonly IEventHandle<T> _handler;

      public DomainEventHandler(IEventHandle<T> handler)
      {
        this._handler = handler;
      }

      public override void Handle(BaseEvent baseEvent) => _handler.Handle((T)baseEvent);
    }
  }
}