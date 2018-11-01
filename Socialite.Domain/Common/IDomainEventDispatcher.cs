using System;
using System.Collections.Generic;
using System.Text;

namespace Socialite.Domain.Common
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseEvent domainEvent);
    }
}
