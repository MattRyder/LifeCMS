using System;
using MediatR;

namespace Socialite.Domain.Common
{
    public abstract class BaseEvent : INotification
    {
        public DateTime RaisedAt { get; protected set; } = DateTime.UtcNow;
    }
}
