using System;

namespace Socialite.Domain.Common
{
    public abstract class BaseEvent
    {
        public DateTime RaisedAt { get; protected set; } = DateTime.UtcNow;
    }
}
